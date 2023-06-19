using BeuStoreApi.Helper;
using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Models.UserDTO;
using BeuStoreApi.Services;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IAuthUser _authService;

        public AuthUserController( IAuthUser authService ) 
        { 
            _authService = authService;

        }

        [HttpPost("register")]
        public async Task<statusDTO> Register([FromBody] RegisterDTO registerDTO)
        {
            return await _authService.Register(registerDTO);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            return await _authService.Login(loginDTO);
        }

        [HttpPost("refresh-token")]
        public async Task<statusDTO> RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            return await _authService.RenewToken(tokenDTO);
        }
        [HttpPost("get-auth")]
        [Authorize]
        public statusDTO getAuth()
        {
            return _authService.getAuth();
        }
        [HttpPost("change-password")]
        [Authorize]
        public async Task<statusDTO> ChangePassword([FromBody] ChangPassworDTO changPassworDTO)
        {
            return await _authService.ChangePassword(changPassworDTO);
        }
    }
}
