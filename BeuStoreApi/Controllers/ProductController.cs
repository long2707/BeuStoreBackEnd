using BeuStoreApi.Models;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeuStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _product;
        public ProductController(IProducts products)
        { 
            
            _product = products;
        
        }
        [HttpGet]
        public async Task<statusDTO> GetAllProduct() 
        { 
            return await _product.getAllProducts();
        }

        [HttpPost]
        public async Task<statusDTO> CreateProductAsync([FromBody] ProductDTO product)
        {

            return await _product.createProductAsync(product);
        }

    }
}
