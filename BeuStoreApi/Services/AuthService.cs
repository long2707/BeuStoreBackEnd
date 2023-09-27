using BeuStoreApi.Constants;
using BeuStoreApi.Entities;
using BeuStoreApi.Helper;
using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BeuStoreApi.Models.UserDTO;


namespace BeuStoreApi.Services
{
    public class AuthService : IAuthUser
    {

        private readonly MyDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly jwtToken _jwtToken;
        private IHttpContextAccessor _contextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor, MyDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, jwtToken jwt)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtToken = jwt;
            _contextAccessor = httpContextAccessor;
        }

        public object Response { get; private set; }

        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                var roleUser = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("firstName", user.firstName),
                    new Claim("lastName", user.lastName),
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
                catch
                {
                    var errorServer = new { message = "Có lỗi xảy ra. Vui lòng thử lại sau" };
                    return new ObjectResult(errorServer)
                    {
                        StatusCode = 500 // Trả về mã lỗi 500 (Internal Server Error)
                    };

                }
                var accesstoken = new JwtSecurityTokenHandler().WriteToken(token);

                //_contextAccessor?.HttpContext?.Response.Cookies.Append("accessToken", accesstoken, new CookieOptions
                // {
                //   Domain= "localhost:3000",
                //     Expires = DateTime.UtcNow.AddHours(1),
                //     Secure = true, // Set to true if using HTTPS
                //     HttpOnly = true ,// Set to true to prevent client-side JavaScript access
                //      SameSite = SameSiteMode.Strict ,
                //      Path="/"
                // });


                // _contextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                // {
                //     Domain= "localhost:3000",
                //     Expires = DateTime.UtcNow.AddDays(1),
                //     Secure = true,
                //     HttpOnly = true,
                //      SameSite = SameSiteMode.Strict ,
                //      Path="/"
                // });
                // _contextAccessor?.HttpContext?.Response.Cookies.Append("role", roleUser[0], new CookieOptions
                // {
                //     Domain= "localhost:3000",
                //     Expires = DateTime.UtcNow.AddHours(1),
                //     Secure = true,
                //     HttpOnly = true,
                //     SameSite = SameSiteMode.Strict,
                //     Path = "/"
                // });
                var data = new
                {
                    AccessToken = accesstoken,
                    RefreshToken = refreshToken,


                };
                return new OkObjectResult(data);
            }
            var errorClient = new { message = "Email/Mật khẩu không đúng!" };
            return new ObjectResult(errorClient)
            {
                StatusCode = 400
            };

        }



        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var userExits = await _userManager.FindByEmailAsync(registerDTO.Email);

            if (userExits != null)

            {

                return new ObjectResult(new { messages = "Email đã được đăng ký" })
                {
                    StatusCode = 400
                };
            }
            var createUser = new User()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                firstName = registerDTO.FirstName,
                lastName = registerDTO.LastName,
            };
            var result = await _userManager.CreateAsync(createUser, registerDTO.Password);
            if (!result.Succeeded)
            {
                return new ObjectResult(new { message = "tạo tài khoản không thành công" })
                {
                    StatusCode = 400
                };
            }
            if (!await _roleManager.RoleExistsAsync(RoleUser.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(RoleUser.Admin));
            }
            if (await _roleManager.RoleExistsAsync(RoleUser.Admin))
            {
                await _userManager.AddToRoleAsync(createUser, RoleUser.Admin);
            }

            return new OkObjectResult(new { message = "Tạo tài khoản thành công" });
        }
        public async Task<IActionResult> RenewToken(TokenDTO tokenDTO)
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
                        return new ObjectResult(new { Message = "Invalid token" })
                        {
                            StatusCode = 403
                        };
                    }
                }

                //check 3: Check accessToken expire?

                var jwtToken = jwtTokenHandler.ReadJwtToken(tokenDTO.accessToken) as JwtSecurityToken;
                var expiryTimeUnix = jwtToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;
                var expiryTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryTimeUnix)).UtcDateTime;
                if (expiryTimeUtc > DateTime.UtcNow)
                {
                    return new ObjectResult(new { Message = "Access token has not yet expired" })
                    {
                        StatusCode = 401
                    };
                    
                }

                //check 4: Check refreshtoken exist in DB
                var storedToken = _dbContext.refreshTokens.FirstOrDefault(x => x.refreshToken == tokenDTO.refreshToken);
                if (storedToken == null)
                {
                    return new ObjectResult(new { Message = "Refresh token does not exist" })
                    {
                        StatusCode = 404
                    };

                }
                //create new token
                var user = await _userManager.FindByIdAsync(storedToken.userId);
                var roleUser = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {

                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Id", user.Id.ToString()),
                     new Claim("Id", user.Id.ToString()),
                    new Claim("firstName", user.firstName),
                    new Claim("lastName", user.lastName),
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
                    return new ObjectResult(new { Message = ex.Message })
                    {
                        StatusCode = 501
                    };

                }
                var accesstoken = new JwtSecurityTokenHandler().WriteToken(token);

                return new OkObjectResult(new { accesstoken, refreshToken });

            }
            catch
            {
                return new ObjectResult(new { Message  = "Something went wrong" })
                {
                    StatusCode = 400
                };

            }
        }
        public IActionResult getAuth()
        {
            string authorizationHeader = _contextAccessor?.HttpContext?.Request?.Headers["Authorization"];
            string token = authorizationHeader.Substring("Bearer ".Length);

            var verifyToken = _jwtToken.Verify(token);
            var idUser = verifyToken.Claims.First(x => x.Type == "Id").Value;
            var emailUser = verifyToken.Claims.First(x => x.Type == "Email").Value;
            var firstName = verifyToken.Claims.First(x => x.Type == "firstName").Value;
            var lastName = verifyToken.Claims.First(x => x.Type == "lastName").Value;
            var roleUser = verifyToken.Claims.First(x => x.Type == "role").Value;

            var getUser = new
            {
                id = idUser,
                email = emailUser,
                firstName = firstName,
                lastName = lastName,
                role = roleUser,
            };

            return new OkObjectResult(new { data = getUser } )
            {
               StatusCode= 200
            };
          

        }
        public async Task<IActionResult> ChangePassword(ChangPassworDTO changPassworDTO)
        {
            var user = await _userManager.FindByIdAsync(changPassworDTO.IdUser);
            if (user == null)
            {
                return new ObjectResult(new { Message = "người dùng không tồn tại" })
                {
                    StatusCode = 404
                };

            }
            var result = await _userManager.ChangePasswordAsync(user, changPassworDTO.oldPassword, changPassworDTO.newPassword);
            if (result.Succeeded)
            {
                return new ObjectResult(new { Messgae = "Cập nhật mật khẩu thành công" })
                {
                    StatusCode = 200
                };

            }
            return new ObjectResult(new { Message = "Mật khẩu cũ nhập sai" })
            {
                StatusCode = 400
            };

        }

    }
}
