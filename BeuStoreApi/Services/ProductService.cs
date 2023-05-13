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
           var products =  await _context.products.Include(p=> p.Tags).Include(p=> p.Categories).ToListAsync();
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
           var productExited = await _context.products.Where(p => p.product_name == product.product_name)?.FirstOrDefaultAsync();
            if(productExited != null)
            {
                return new statusDTO()
                {
                    Success = false,
                    data= new
                    {
                        Message = "sản phẩm đã được tạo"
                    }
                };
            }
            var tags = new List<Tags>();
            foreach(var item in product.tags)
            {
                var tag= await _context.tags.Where(t => t.tag_name == item)?.FirstOrDefaultAsync();
                if(tag == null)
                {
                     tag = new Tags()
                    {
                        tag_name = item
                    };
                    _context.Add(tag);
                }
                tags.Add(tag);
            }
            var categories = new List<Categories>();
            foreach(var item in product.categories)
            {
                var  category = await _context.categories.Where(c => c.category_Name == item).FirstOrDefaultAsync();
                categories.Add(category);

            }
            var newProduct = new Products()
            {
                product_name = product.product_name,
                SKU= product.SKU,
                product_description = product.product_description,
                regular_price = product.regular_price,
                discount_price = product.discount_price,
                Tags = tags,
                Categories = categories,
               
            };
            _context.Add(newProduct);
            await _context.SaveChangesAsync();
            return new statusDTO()
            {
                Success = true,
                data = new
                {
                    Message =tags.ToArray(),
                    categories = categories.ToArray()
                }
            };
        }
    }
}
