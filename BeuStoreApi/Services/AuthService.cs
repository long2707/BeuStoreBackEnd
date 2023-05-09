using BeuStoreApi.Constants;
using BeuStoreApi.Entities;
using BeuStoreApi.Helper;
using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeuStoreApi.Services
{
    public class AuthService : IAuthStaff
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<Staff> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly jwtToken _jwtToken;
        public AuthService(MyDbContext dbContext , UserManager<Staff> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, jwtToken jwt) 
        { 
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtToken = jwt;
        }
        public async Task<statusDTO> LoginStaff(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password)) 
            {
                var roleUser = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email)
                };

                foreach (var userRole in roleUser)
                {
                    authClaims.Add(new Claim("role", userRole));
                }

                var token = _jwtToken.GetToken(authClaims);
                var refreshToken = _jwtToken.RefreshToken();
                var tokenInfo = _dbContext.refreshTokens.FirstOrDefault(a => a.userId == user.Id);

                if (tokenInfo == null)
                {
                    var info = new RefreshToken
                    {
                        Id = new Guid(),
                        userId = user.Id,
                        refreshToken= refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(1),
                    };
                    _dbContext.refreshTokens.Add(info);
                }
                else
                {
                    tokenInfo.refreshToken= refreshToken;
                    tokenInfo.RefreshTokenExpiry= DateTime.Now.AddDays(1);
                }
                try
                {
                    _dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    return new statusDTO() { 
                    Success= false,
                    data= new
                    {
                        Message= ex.Message,
                    }
                    };

                }
                return new statusDTO()
                {
                   Success= true,
                   data= new
                   {
                       AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                       RefreshToken =  refreshToken,
                     
                   }

                   
                };
            }
            return new statusDTO()
            {
                Success = false,
                data = new
                {
                    Message = "Email/Mật khẩu không đúng"
                }
            };
        }



        public async Task<statusDTO> RegisterStaff(RegisterDTO registerDTO)
        {
            var userExits =await _userManager.FindByEmailAsync(registerDTO.Email);
            if(userExits != null)
            
            {
                var data = new statusDTO()
                {
                    Success = false,
                    data= new
                    {
                        Message = "Email đã được đăng ký"
                    }
                };
                return (data) ;
            }
            var createUser = new Staff()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                
                
            };
            var result = await _userManager.CreateAsync(createUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                return new  statusDTO()
                {
                   Success= false,
                   data= new
                   {
                       Message = "tạo tài khoản không thành công"
                   }
                };
            }
            if(! await _roleManager.RoleExistsAsync(RoleStaff.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleStaff.Admin));
            }
            if( await _roleManager.RoleExistsAsync(RoleStaff.Admin))
            {
                await _userManager.AddToRoleAsync(createUser,RoleStaff.Admin);
            }
            return new statusDTO()
            {
                Success = true,
               data= new
               {
                   Message = "Tạo tài khoản thành công"
               }
            };
        }
        public async Task<statusDTO> RenewToken(TokenDTO tokenDTO)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var tokenValidateParam = new TokenValidationParameters
            {
                //tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false 
            };
            try
            {
                //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenDTO.accessToken, tokenValidateParam, out var validatedToken);

                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)
                    {
                        return new statusDTO()
                        {
                            Success = false,
                            data = new
                            {
                                Message = "Invalid token"
                            }
                        };
                    }
                }

                //check 3: Check accessToken expire?

                var jwtToken = jwtTokenHandler.ReadJwtToken(tokenDTO.accessToken) as JwtSecurityToken;
                var expiryTimeUnix = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                var expiryTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryTimeUnix)).UtcDateTime;
                if (expiryTimeUtc > DateTime.UtcNow)
                {
                    return new statusDTO()
                    {
                        Success = false,
                       data = new { Message = "Access token has not yet expired" }
                    };
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _dbContext.refreshTokens.FirstOrDefault(x => x.refreshToken == tokenDTO.refreshToken);
                if (storedToken == null)
                {
                    return new statusDTO()
                    {
                        Success = false,
                       data= new
                       {
                           Message = "Refresh token does not exist"
                       }
                    };
                }
                //create new token
                var user = await _userManager.FindByIdAsync(storedToken.userId);
                var roleUser = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email)
                };

                foreach (var userRole in roleUser)
                {
                    authClaims.Add(new Claim("role", userRole));
                }

                var token = _jwtToken.GetToken(authClaims);
          
                var refreshToken = _jwtToken.RefreshToken();
                var tokenInfo = _dbContext.refreshTokens.FirstOrDefault(a => a.userId == user.Id);

                if (tokenInfo == null)
                {
                    var info = new RefreshToken
                    {
                        Id = new Guid(),
                        userId = user.Id,
                        refreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.Now.AddDays(1),
                    };
                    _dbContext.refreshTokens.Add(info);
                }
                else
                {
                    tokenInfo.refreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.Now.AddDays(1);
                }
                try
                {
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new statusDTO()
                    {
                        Success = false,
                        data = new
                        {
                            Message = ex.Message,
                        }
                    };

                }
                return new statusDTO()
                {
                    Success = true,
                    data = new
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        RefreshToken = refreshToken,
                        Exipration = token.ValidTo
                    }


                };
            }
            catch (Exception ex)
            {
                return new statusDTO
                {
                    Success = false,
                    data = new { Message = "Something went wrong" }
                };
            }
        }
 

    }
}
