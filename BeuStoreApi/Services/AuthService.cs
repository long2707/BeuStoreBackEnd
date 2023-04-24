using BeuStoreApi.Constants;
using BeuStoreApi.Entities;
using BeuStoreApi.Helper;
using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeuStoreApi.Services
{
    public class AuthService : IAuthStaff
    {
        private readonly UserManager<Staff> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly jwtToken _jwtToken;
        public AuthService(UserManager<Staff> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, jwtToken jwt) 
        { 
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
                };

                foreach (var userRole in roleUser)
                {
                    authClaims.Add(new Claim("role", userRole));
                }

                var token = _jwtToken.GetToken(authClaims);
                return new statusDTO()
                {
                   Success= true,
                   data= new
                   {
                       accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                       role = roleUser,
                       useId = user.Id
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
    }
}
