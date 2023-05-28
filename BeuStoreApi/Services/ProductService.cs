using BeuStoreApi.Entities;
using BeuStoreApi.Models;
using BeuStoreApi.Services.interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BeuStoreApi.Services
{
    public class ProductService : IProducts
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySettings;
        public ProductService(MyDbContext dbContext, IConfiguration configuration) 
        {
            _context = dbContext;
            _configuration = configuration;
            _cloudinarySettings = _configuration.GetSection("CloudinarySetting").Get<CloudinarySettings>();
            Account account = new Account
              (  _cloudinarySettings.CloudName,
              _cloudinarySettings.ApiKey,
              _cloudinarySettings.ApiSecret
                
            );
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure= true;
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
        public async Task<statusDTO> createProductAsync( ProductDTO product, List<IFormFile> formFiles)
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
            // tags
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

            //categories
            var categories = new List<Categories>();
            foreach(var item in product.categories)
            {
                var  category = await _context.categories.Where(c => c.category_Name == item).FirstOrDefaultAsync();
                categories.Add(category);

            }

            //image

            var files = product.thumbails;
            if(files == null) return new statusDTO() { Success= false, data= new { Message = "ảnh là bắt buôc"} };

            var thumbails = new List<Gallerles>();

            foreach (var item in files)
            {
                if(item.Length > 0) { 


                var uploadResult = new ImageUploadResult();
                string titleImage = Regex.Replace(product.product_name, @"\s", "-");
                var urlImage = Guid.NewGuid() + "_" + titleImage;
                using (var stream = item.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(urlImage, stream),
                        PublicId = urlImage,
                        DisplayName = titleImage,
                        UniqueFilename = true

                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                }
   
                var newImage = new Gallerles()
                {
                    id = new Guid(),
                    product_id= new Guid(),
                    urlImage= uploadResult.SecureUrl.ToString(),
                };
                thumbails.Add(newImage);
                }
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
                Gallerles= thumbails
               
            };
            _context.Add(newProduct);
            await _context.SaveChangesAsync();
            return new statusDTO()
            {
                Success = true,
                data = new
                {
                    Message =tags.ToArray(),
                    categories = categories.ToArray(),
                    thumbail= thumbails.ToArray()
                }
            };
        }
    }
}
