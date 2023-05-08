using BeuStoreApi.Entities;
using BeuStoreApi.Models;
using BeuStoreApi.Services.interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeuStoreApi.Services
{
    public class ProductService : IProducts
    {
        private readonly MyDbContext _context;
        public ProductService(MyDbContext dbContext) 
        {
            _context = dbContext;
        }
        public async Task<statusDTO> getAllProducts()
        {
           var products =  await _context.products.ToListAsync();
            return new statusDTO()
            {
                Success = true,
                data =
                new
                {
                    data = products
                }
            };
        }
        public async Task<statusDTO> createProductAsync( ProductDTO product)
        {
           
            return new statusDTO()
            {
                Success = true,
                data = new
                {
                    Message = "f"
                }
            };
        }
    }
}
