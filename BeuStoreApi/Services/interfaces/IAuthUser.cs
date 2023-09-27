using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Models.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Services.interfaces
{
    public interface IAuthUser
    {
        Task<IActionResult> Register(RegisterDTO registerDTO);
        Task<IActionResult> Login(LoginDTO loginDTO);
        Task<IActionResult> RenewToken(TokenDTO tokenDTO);
        IActionResult getAuth();
        Task<IActionResult> ChangePassword(ChangPassworDTO changPassworDTO);

    }
}
