using BeuStoreApi.Entities;
using BeuStoreApi.Models;
using BeuStoreApi.Services.interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        public async Task<statusDTO> FetchProducts(int page=1, int pageSize =10)
        {
            var queryable = _context.products;
            var count = await queryable.CountAsync();
           var products =  await queryable.AsNoTracking()
                                                  .Include(x=> x.Categories)
                                                  .Include(x=>x.Gallerles)
                                                  .Include(x=> x.Attrbutes)
                                                  .ThenInclude(x=> x.attrbuteValues)
                                                  .Skip((page - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .Select(x=> new {x.Id,
                                                                   x.product_name, 
                                                                   x.product_description, 
                                                                   x.regular_price, 
                                                                   x.discount_price, 
                                                                   x.quantity, 
                                                                   x.SKU,
                                                                   category = x.Categories.Select(x=> new {x.categoryId, x.category_Name}).ToList(),
                                                                   tags = x.Tags.Select(x=> new {x.id, x.tag_name}).ToList(),
                                                                   image=x.Gallerles.Select(a=> new {a.id, a.urlImage}).ToList(),
                                                                   attributes = x.Attrbutes.Select(a=> new {a.id, a.atrribute_name, s= a.attrbuteValues.Select(x=> new {x.Id, x.attribute_value}).ToList()}).ToList(),
                                                                   
                                                  }).ToListAsync();
            return new statusDTO()
            {
                Success = true,
                data =
                new
                {
                    count=count,
                    products
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
            // tags
            var tags = new List<Tags>();
            if(product.tags.Length == 0)
            {
                return new statusDTO()
                {
                    Success = false,
                    data = new
                    {
                        message = "Nhập tối thiểu 3 tags"
                    }
                };
            }
            foreach (var item in product.tags)
            {
                var tag = await _context.tags.Where(t => t.tag_name == item)?.FirstOrDefaultAsync();
                if (tag == null)
                {
                    tag = new Tags()
                    {
                        tag_name = item
                    };
                    _context.Add(tag);
                }
                tags.Add(tag);
            }
            if (tags.Count() < 3)
            {
                return new statusDTO()
                {
                    Success = false,
                    data = new
                    {
                        message = "Nhập tối thiểu thêm "+ ( 3- tags.Count) +" tags"
                    }
                };
            }
            //categories
            if(product.categories.Length == 0)
            {
                return new statusDTO()
                {
                    Success = false,
                    data = new
                    {
                        Meassage = "Nhập danh mục sản phẩm"
                    }
                };
            }
            var categories = new List<Categories>();
            foreach(var item in product.categories)
            {
                var  category = await _context.categories.Where(c => c.category_Name == item).FirstOrDefaultAsync();
                categories.Add(category);

            }

            //image

            var files = product.thumbails;
            if (files == null) return new statusDTO() { Success = false, data = new { Message = "ảnh là bắt buôc" } };

            var thumbails = new List<Gallerles>();

            foreach (var item in files)
            {
                if (item.Length > 0)
                {


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
                        product_id = new Guid(),
                        urlImage = uploadResult.SecureUrl.ToString(),
                    };
                    thumbails.Add(newImage);
                }
            }
           // attribute

            
                var attributes = new List<Attrbutes>();
            //    "size": ["M", "XL"],
            //    "colors": ["black", "white"]
            //}
            var dataAttribute = product.attributeName;
            if (product.attributeName != null)
            {
                for (int i = 0; i < dataAttribute.Count; i++)
                {
                    var attributeValues = new List<AttrbuteValue>();
                    foreach (var value in dataAttribute[i]?.valueAttribute)
                    {
                        var newAtributeValue = new AttrbuteValue()
                        {
                            Id = new Guid(),
                            attribute_id = new Guid(),
                            attribute_value = value.ToString()
                        };
                        attributeValues.Add(newAtributeValue);
                    }
                    attributes.Add(new Attrbutes()
                    {
                        id = new Guid(),
                        atrribute_name = dataAttribute[i].Name,
                        attrbuteValues = attributeValues,
                        create_at = DateTime.UtcNow
                    });
                }
            }


            try
            {
                var newProduct = new Products()
                {
                    product_name = product.product_name,
                    SKU = product.SKU,
                    product_description = product.product_description,
                    regular_price = product.regular_price,
                    discount_price = product.discount_price,
                    Tags = tags,
                    Categories = categories,
                    Gallerles= thumbails,
                    Attrbutes = attributes

                };
                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return new statusDTO()
                {
                    Success = true,
                    data = newProduct
                };
            }
            catch
            {
                return new statusDTO()
                {
                    Success = false,
                    data = new
                    {
                        Message = "Có lỗi xảy ra!. Thử lại sau"
                    }
                };
            }
        }
    }
}
