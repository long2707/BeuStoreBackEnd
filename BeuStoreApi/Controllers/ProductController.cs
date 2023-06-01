using BeuStoreApi.Models;
using BeuStoreApi.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpGet("get-product")]
        public async Task<statusDTO> FetchProducts([FromQuery] int page=1, [FromQuery] int pageSize=10) 
        { 
            return await _product.FetchProducts(page, pageSize);
        }

        [HttpPost("create-product"), Authorize(Roles = "admin")]
        public async Task<statusDTO> CreateProductAsync([FromForm] ProductDTO product)
        {

            return await _product.createProductAsync(product);
        }

    }
}
