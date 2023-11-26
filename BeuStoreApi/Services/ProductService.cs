//using BeuStoreApi.Entities;
//using BeuStoreApi.Helper;
//using BeuStoreApi.Models;
//using BeuStoreApi.Models.ProductsDTO;
//using BeuStoreApi.Services.interfaces;
//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Text.RegularExpressions;

//namespace BeuStoreApi.Services
//{
//    public class ProductService : IProducts
//    {
//        private readonly MyDbContext _context;
//        private readonly UploadImage _uploadImage;
       
//        public ProductService(MyDbContext dbContext, UploadImage uploadImage)
//        {
//            _context = dbContext;
//            _uploadImage = uploadImage;
          
          
//        }
//        public async Task<statusDTO> FetchProducts(int page = 1, int pageSize = 10)
//        {
//            var getProducts = _context.products;
//            var count = await getProducts.CountAsync();
//            var products = await getProducts.AsNoTracking()

//                                                   .Include(x => x.Galleries)

//                                                   .Skip((page - 1) * pageSize)
//                                                   .Take(pageSize)
//                                                   .Select(x => new
//                                                   {
//                                                       x.Id,
//                                                       x.product_name,

//                                                       x.sale_price,
//                                                       x.compare_price,
//                                                       x.quantity,
//                                                       x.SKU,
//                                                       image = x.Galleries.Select(a => a.urlImage).ToList(),


//                                                   }).ToListAsync();
//            return new statusDTO()
//            {
//                Success = true,
//                data =
//                new
//                {
//                    count = count,
//                    products
//                }
//            };
//        }
//        public async Task<statusDTO> DetailProduct(Guid productid)
//        {
//            var result = await _context.products.AsNoTracking()
//                                                  .Include(x => x.ProductCategories)
//                                                  .Include(x => x.Galleries)
//                                                  .Include(x => x.ProductAttributes)
//                                                  .ThenInclude(x => x.Attrbutes)
//                                                  .Select(x => new
//                                                  {
//                                                      x.Id,
//                                                      x.product_name,
//                                                      x.product_description,
//                                                      x.sale_price,
//                                                      x.compare_price,
//                                                      x.quantity,
//                                                      x.SKU,
//                                                      category = x.ProductCategories.Select(x => x.category_Name).ToList(),
//                                                      tags = x.Tags.Select(x => x.tag_name).ToList(),
//                                                      image = x.Galleries.Select(a => a.urlImage).ToList(),
//                                                      //   attributes = x.Attrbutes.Select(a => new { a.id, a.atrribute_name, data = a.attrbuteValues.Select(x => x.attribute_value).ToList() }).ToList(),

//                                                  }).FirstOrDefaultAsync(x => x.Id == productid);
//            if (result != null)
//            {
//                return new statusDTO()
//                {
//                    Success = true,
//                    data = result
//                };
//            }
//            return new statusDTO()
//            {
//                Success = false,
//                data = new
//                {
//                    Message = "Không tìm thấy sản phẩm"
//                }
//            };
//        }
//        public async Task<IActionResult> createProductAsync(ProductDTO product)
//        {
//            var productExited = await _context.products.FirstOrDefaultAsync(p => p.product_name == product.product_name);
//            if (productExited != null)
//            {

//                return new ObjectResult(new { Message = "sản phẩm đã được tạo" })
//                {
//                    StatusCode = 409
//                };
//            }
//            // tags
//            var tags = new List<Tags>();
//            if (product.tags == null || product.tags.Length == 0)
//            {

//                return new ObjectResult(new { Message = "Nhập tối thiểu 3 tags" })
//                {
//                    StatusCode = 400
//                };

//            }
//            foreach (var item in product.tags)
//            {
//                var tag = await _context.tags.FirstOrDefaultAsync(t => t.tag_name == item);
//                if (tag == null)
//                {
//                    tag = new Tags()
//                    {
//                        tag_name = item
//                    };
//                    _context.Add(tag);
//                }
//                tags.Add(tag);
//            }
//            if (tags.Count() < 3)
//            {
//                return new ObjectResult(new {Message = "Nhập tối thiểu thêm " + (3 - tags.Count) + " tags" })
//                {
//                    StatusCode = 400
//                };
              
//            }
//            //categories
//            if (product.categories == null || product.categories.Length == 0)
//            {

//                return new ObjectResult(new
//                {
//                    Meassage = "Vui lòng chọn danh mục sản phẩm"
//                })
//                {
//                    StatusCode = 400
//                };
              
//            }
//            var categories = new List<Categories>();
//            foreach (var itemCategory in product.categories)
//            {
//                var category = await _context.categories.FirstOrDefaultAsync(c => c.categoryId == itemCategory);
//                if (category != null)
//                {
//                    categories.Add(category);
//                }

//            }

//            //image

//            var files = product.thumbails;
//            if (files == null) return new ObjectResult(new { Message = "ảnh sản phẩm là bắt buộc" });

//            var thumbnails = new List<Gallery>();

//            foreach (var item in files)
//            {
//                if (item.Length > 0)
//                {


//                    var uploadResult = await _uploadImage.UploadImages(item, product.product_name);

//                    var newImage = new Gallery()
//                    {
//                        id = new Guid(),
//                        urlImage = uploadResult.SecureUrl + "",
//                        PublicId = uploadResult.PublicId + ""
//                    };
//                    thumbnails.Add(newImage);
//                }
//            }
//            // attribute


//            var attributes = new List<Attrbutes>();
//            //    "size": ["M", "XL"],
//            //    "colors": ["black", "white"]
//            //}
//            var dataAttribute = product.attribute;
//            if (dataAttribute != null)
//            {
//                for (int i = 0; i < dataAttribute.Count; i++)
//                {
//                    var attributeValues = new List<AttrbuteValue>();
//                    foreach (var value in dataAttribute[i]?.valueAttribute)
//                    {

//                        var newAtributeValue = new AttrbuteValue()
//                        {
//                            Id = new Guid(),
//                            attribute_value = value.ToString()
//                        };
//                        attributeValues.Add(newAtributeValue);
//                    }
//                    attributes.Add(new Attrbutes()
//                    {
//                        id = new Guid(),
//                        atrribute_name = dataAttribute[i].attribute_name,
//                        attrbuteValues = attributeValues,

//                    });
//                }
//            }


//            try
//            {
//                var newProduct = new Products()
//                {
//                    product_name = product.product_name,
//                    SKU = product.SKU,
//                    product_description = product.product_description,
//                    compare_price = product.regular_price,
//                    sale_price = product.discount_price,
//                    quantity = product.quantity,
//                    created_by = product.created_by,
//                    Tags = tags,
//                    ProductCategories = categories,
//                    Galleries = thumbnails,
//                 //   ProductAttributes = attributes

//                };
//                _context.Add(newProduct);
//                await _context.SaveChangesAsync();
//                return new OkObjectResult(new { Message = "Tạo sản phẩm thành công" });
//            }
//            catch
//            {
//                return new ObjectResult(new { Message = "Cõ lỗi xảy ra vui lòng thử lại sau!" })
//                {
//                    StatusCode = 500
//                };
//            }
//        }
//        public async Task<statusDTO> DeleteProduct(Guid productId)
//        {
//            var result = await _context.products.AsNoTracking()
//                                                  .Include(x => x.ProductCategories)
//                                                  .Include(x => x.Galleries)
//                                                  .Include(x => x.ProductAttributes)
//                                                  //.ThenInclude(x => x.attrbuteValues)
//                                                  .FirstOrDefaultAsync(x => x.Id == productId);
//            if (result == null)
//            {
//                return new statusDTO()
//                {
//                    Success = false,
//                    data = new
//                    {
//                        Message = "Sản phẩm không tồn tại"
//                    }
//                };
//            }
//            _context.products.Remove(result);
//            _context.SaveChanges();
//            return new statusDTO()
//            {
//                Success = true,
//                data = new
//                {
//                    Message = "Xóa sản phẩm thành công"
//                }
//            };
//        }
//        public async Task<statusDTO> UpdateProductAsync(UpdateProductDTO updateProduct, Guid productId)
//        {
//            var productExist = await _context.products
//                                                  .Include(x => x.Tags)
//                                                  .Include(x => x.ProductCategories)
//                                                  .Include(x => x.Galleries)
//                                                  .Include(x => x.ProductAttributes)
//                                                  //.ThenInclude(x => x.attrbuteValues)
//                                                  .FirstOrDefaultAsync(x => x.Id == productId);

//            if (productExist != null)
//            {
//                productExist.Tags.Clear();
//                var tags = new List<Tags>();
//                if (updateProduct.tags.Count() == 0)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            message = "Nhập tối thiểu 3 tags"
//                        }
//                    };
//                }
//                foreach (var item in updateProduct.tags)
//                {
//                    var tag = await _context.tags.FirstOrDefaultAsync(t => t.tag_name == item);
//                    if (tag == null)
//                    {
//                        tag = new Tags()
//                        {
//                            tag_name = item
//                        };
//                        _context.Add(tag);
//                    }

//                    tags.Add(tag);
//                    productExist.Tags.Add(tag);
//                }
//                if (tags.Count() < 3)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            message = "Nhập tối thiểu thêm " + (3 - tags.Count) + " tags"
//                        }
//                    };
//                }

//                //categories
//                if (updateProduct.updateCategories.Count == 0)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            Meassage = "Nhập danh mục sản phẩm"
//                        }
//                    };
//                }
//                productExist.ProductCategories.Clear();
//                var categories = new List<Categories>();
//                foreach (var item in updateProduct.updateCategories)
//                {
//                    var category = await _context.categories.FirstOrDefaultAsync(c => c.categoryId == item.CategoryId);
//                    if (category == null)
//                    {
//                        return new statusDTO()
//                        {
//                            Success = false,
//                            data = new
//                            {
//                                Message = "Danh mục này không tồn tại!"
//                            }

//                        };
//                    }
//                    productExist.ProductCategories.Add(category);


//                }


//                //thubails
//                productExist.Galleries.Clear();
//                var thumbailUrls = updateProduct.thumbailUrls;
//                var thumbailFiles = updateProduct.thumbailFiles;

//                if (thumbailFiles == null && thumbailUrls == null)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            Message = "Nhập ít nhất 1 ảnh!"
//                        }

//                    };
//                }
//                if (thumbailUrls != null)
//                {
//                    foreach (var item in thumbailUrls)
//                    {
//                        var thumbail = new Gallerles()
//                        {
//                            urlImage = item
//                        };
//                        productExist.Gallerles.Add(thumbail);
//                    }
//                }
//                if (thumbailFiles != null)
//                {
//                    foreach (var item in thumbailFiles)
//                    {

//                        if (item.Length > 0)
//                        {


//                            var uploadResult = new ImageUploadResult();
//                            string titleImage = Regex.Replace(updateProduct.product_name, @"\s", "-");
//                            var urlImage = Guid.NewGuid() + "_" + titleImage;
//                            using (var stream = item.OpenReadStream())
//                            {
//                                var uploadParams = new ImageUploadParams()
//                                {
//                                    File = new FileDescription(urlImage, stream),
//                                    PublicId = urlImage,
//                                    DisplayName = titleImage,
//                                    UniqueFilename = true

//                                };

//                                uploadResult = await _cloudinary.UploadAsync(uploadParams);
//                            }

//                            var newImage = new Gallerles()
//                            {
//                                id = new Guid(),
//                                product_id = new Guid(),
//                                PublicId = uploadResult.PublicId,
//                                urlImage = uploadResult.SecureUrl.ToString(),
//                            };

//                            productExist.Gallerles.Add(newImage);
//                        }
//                    }
//                }

//                //var files = product.thumbails;
//                //if (files == null) return new statusDTO() { Success = false, data = new { Message = "ảnh là bắt buôc" } };

//                //var thumbails = new List<Gallerles>();

//                //foreach (var item in files)
//                //{
//                //    if (item.Length > 0)
//                //    {


//                //        var uploadResult = new ImageUploadResult();
//                //        string titleImage = Regex.Replace(product.product_name, @"\s", "-");
//                //        var urlImage = Guid.NewGuid() + "_" + titleImage;
//                //        using (var stream = item.OpenReadStream())
//                //        {
//                //            var uploadParams = new ImageUploadParams()
//                //            {
//                //                File = new FileDescription(urlImage, stream),
//                //                PublicId = urlImage,
//                //                DisplayName = titleImage,
//                //                UniqueFilename = true

//                //            };

//                //            uploadResult = await _cloudinary.UploadAsync(uploadParams);
//                //        }

//                //        var newImage = new Gallerles()
//                //        {
//                //            id = new Guid(),
//                //            product_id = new Guid(),
//                //            a= uploadResult.PublicId,
//                //            urlImage = uploadResult.SecureUrl.ToString(),
//                //        };
//                //        thumbails.Add(newImage);
//                //    }
//                //}
//                // attribute

//                productExist.Attrbutes.Clear();
//                var attributes = new List<Attrbutes>();
//                //    "size": ["M", "XL"],
//                //    "colors": ["black", "white"]
//                //}
//                var dataAttribute = updateProduct.updateAttributes;

//                if (dataAttribute == null)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            Message = "Nhập phân loại hàng"
//                        }
//                    };
//                }
//                for (int i = 0; i < dataAttribute.Count; i++)
//                {
//                    var attributeExist = _context.Attrbutes.FirstOrDefault(a => a.id == dataAttribute[i].Id);

//                    var attributeValues = new List<AttrbuteValue>();
//                    if (dataAttribute[i].updateAttributeValues == null)
//                    {
//                        return new statusDTO()
//                        {
//                            Success = false,
//                            data = new
//                            {
//                                Message = "Nhập phân loại hàng sản phẩm cho" + dataAttribute[i].Name
//                            }
//                        };
//                    }
//                    foreach (var value in dataAttribute[i]?.updateAttributeValues)
//                    {
//                        var newAtributeValue = new AttrbuteValue()
//                        {
//                            Id = new Guid(),

//                            attribute_value = value.Name
//                        };
//                        attributeValues.Add(newAtributeValue);
//                    }
//                    if (attributeExist != null)
//                    {
//                        attributeExist.attrbuteValues.Clear();
//                        attributeExist.atrribute_name = dataAttribute[i].Name;
//                        attributeExist.attrbuteValues = attributeValues;
//                        productExist.Attrbutes.Add(attributeExist);
//                    }
//                    else
//                    {
//                        var newAttribute = new Attrbutes()
//                        {
//                            id = new Guid(),
//                            atrribute_name = dataAttribute[i].Name,
//                            attrbuteValues = attributeValues,

//                        };
//                        _context.Add(newAttribute);
//                        productExist.Attrbutes.Add(newAttribute);
//                    }
//                }
//                try
//                {

//                    productExist.product_name = updateProduct.product_name;
//                    productExist.SKU = updateProduct.SKU;
//                    productExist.product_description = updateProduct.product_description;
//                    productExist.compare_price = updateProduct.regular_price;
//                    productExist.sale_price = updateProduct.discount_price;
//                    productExist.quantity = updateProduct.quantity;

//                    //  _context.Update(productExist);
//                    await _context.SaveChangesAsync();
//                    return new statusDTO()
//                    {
//                        Success = true,
//                        data = new
//                        {
//                            Message = "Cập nhật sản phẩm thành công"
//                        }
//                    };
//                }
//                catch (Exception ex)
//                {
//                    return new statusDTO()
//                    {
//                        Success = false,
//                        data = new
//                        {
//                            Message = ex
//                        }
//                    };
//                }
//            }
//            return new statusDTO()
//            {
//                Success = false,
//                data = new
//                {
//                    Message = "Sản phẩm không tồn tại"
//                }
//            };
//        }

//    }
//}