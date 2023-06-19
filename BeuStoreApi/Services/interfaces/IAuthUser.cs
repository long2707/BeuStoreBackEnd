using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using BeuStoreApi.Models.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Services.interfaces
{
    public interface IAuthUser
    {
        Task<statusDTO> Register(RegisterDTO registerDTO);
        Task<IActionResult> Login(LoginDTO loginDTO);
        Task<statusDTO> RenewToken(TokenDTO tokenDTO);
        statusDTO getAuth();
        Task<statusDTO> ChangePassword(ChangPassworDTO changPassworDTO);

    }
}
