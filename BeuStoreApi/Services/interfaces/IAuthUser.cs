using BeuStoreApi.Models;
using BeuStoreApi.Models.StaffDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Services.interfaces
{
    public interface IAuthUser
    {
        Task<statusDTO> Register(RegisterDTO registerDTO);
        Task<statusDTO> Login(LoginDTO loginDTO);
        Task<statusDTO> RenewToken(TokenDTO tokenDTO);
        statusDTO getAuth();

    }
}
