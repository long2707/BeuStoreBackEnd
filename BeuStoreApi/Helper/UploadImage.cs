using BeuStoreApi.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Text.RegularExpressions;

namespace BeuStoreApi.Helper
{
    public class UploadImage
    {
        private readonly CloudinarySettings _settings;
        private readonly IConfiguration _configuration;
        private readonly Cloudinary _cloudinary;
        private UploadImage( IConfiguration configuration)
        {
            _configuration = configuration;
            _settings = _configuration.GetSection("CloudinarySetting").Get<CloudinarySettings>();
            Account account = new Account
                (
                _settings.ApiSecret,
                _settings.ApiKey,
                _settings.CloudName
                ) ;
            _cloudinary = new Cloudinary ( account ) ;
            _cloudinary.Api.Secure = true ;
        }
        public async Task<UploadResult> UploadImages(IFormFile file, string fileName)
        {
            var uploadResult = new ImageUploadResult();
            string titleImage = Regex.Replace(fileName, @"\s", "-"); 

            //                    }
            var urlImage = Guid.NewGuid() + "_" + titleImage;
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(urlImage, stream),
                    PublicId = urlImage,
                    DisplayName = titleImage,
                    UniqueFilename = true

                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            }
        }
    }
}
