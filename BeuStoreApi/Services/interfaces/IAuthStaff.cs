using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Services.interfaces
{
    public interface IAuthStaff
    {
        Task<statusDTO> RegisterStaff(RegisterDTO registerDTO);
        Task<statusDTO> LoginStaff(LoginDTO loginDTO);
        Task<statusDTO> RenewToken(TokenDTO tokenDTO);

    }
}
