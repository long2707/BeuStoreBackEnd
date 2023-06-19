using BeuStoreApi.Models;

namespace BeuStoreApi.Services.interfaces
{
    public interface ICart
    {
        Task<statusDTO> CreateCartAsync();
        Task<statusDTO> UpdateCartAsync();
        Task<statusDTO> DeleteCartAsync();
    }
}
