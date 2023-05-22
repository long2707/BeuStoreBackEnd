using BeuStoreApi.Helper;
using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Services;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Controllers
{
    [Route("api/auth/staff")]
    [ApiController]
    public class AuthStaffController : ControllerBase
    {
        private readonly IAuthStaff _authService;
        private readonly jwtToken _jwtToken;
        public AuthStaffController( IAuthStaff authService, jwtToken jwtToken ) 
        { 
            _authService = authService;
            _jwtToken = jwtToken;
        }

        [HttpPost("register")]
        public async Task<statusDTO> RegisterStaff([FromBody] RegisterDTO registerDTO)
        {
            return await _authService.RegisterStaff(registerDTO);
        }
        [HttpPost("login")]
        public async Task<statusDTO> LoginStaff([FromBody] LoginDTO loginDTO)
        {
            return await _authService.LoginStaff(loginDTO);
        }

        [HttpPost("refresh-token")]
        public async Task<statusDTO> RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            return await _authService.RenewToken(tokenDTO);
        }
        [HttpPost("get-auth")]
        [Authorize]
        public statusDTO getAuth(string token)
        {
            return _authService.getAuth(token);
        }
    }
}
