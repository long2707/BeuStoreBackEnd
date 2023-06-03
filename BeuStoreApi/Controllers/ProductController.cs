using BeuStoreApi.Models;
using BeuStoreApi.Models.ProductsDTO;
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
        [HttpGet("detail-product")]
        public async Task<statusDTO> DetailtProduct(Guid productId)
        {
            return await _product.DetailProduct(productId);
        }

        [HttpPost("create-product"), Authorize(Roles = "admin")]
        public async Task<statusDTO> CreateProductAsync([FromForm] ProductDTO product)
        {

            return await _product.createProductAsync(product);
        }
        [HttpDelete("delete-product"), Authorize(Roles ="admin")]

        public async Task<statusDTO> DeleteProduct(Guid productId)
        {
            return await _product.DeleteProduct(productId);
        }
        [HttpPut("update-product")]
        public async Task<statusDTO> UpdateProduct( Guid productId, [FromBody] UpdateProductDTO updateProduct)
        {
            return await _product.UpdateProductAsync(updateProduct, productId);
        }

    }
}
