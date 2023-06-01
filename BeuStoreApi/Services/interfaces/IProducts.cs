using BeuStoreApi.Models;
using BeuStoreApi.Models.ProductsDTO;

namespace BeuStoreApi.Services.interfaces
{
    public interface IProducts
    {
        Task<statusDTO> FetchProducts(int page=1, int pageSize=10);
        Task<statusDTO> DetailProduct(Guid productId);
        Task<statusDTO> createProductAsync(ProductDTO product);
        Task<statusDTO> DeleteProduct(Guid productId);
    }
}
