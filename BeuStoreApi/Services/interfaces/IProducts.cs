using BeuStoreApi.Models;

namespace BeuStoreApi.Services.interfaces
{
    public interface IProducts
    {
        Task<statusDTO> getAllProducts();
        Task<statusDTO> createProductAsync(ProductDTO product);
    }
}
