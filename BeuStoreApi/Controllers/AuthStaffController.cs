using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Services;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Controllers
{
    [Route("api/auth/staff")]
    [ApiController]
    public class AuthStaffController : ControllerBase
    {
        private readonly IAuthStaff _authService;
        public AuthStaffController( IAuthStaff authService ) 
        { 
            _authService = authService;
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
    }
}
