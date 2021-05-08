
using Application.Interfaces;
using System.Threading.Tasks;
using Application.Photos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;

namespace Infrastructure.Photos
{
      public class PhotoAccessor : IPhotoAccessor
      {

            private readonly Cloudinary _cloudianry;
            public PhotoAccessor(IOptions<CloudinarySettings>config)
            {
                  var account = new Account(
                      config.Value.CloudName,
                      config.Value.ApiKey,
                      config.Value.ApiSecret
                  );
                  _cloudianry = new Cloudinary(account);
            }

            public async Task<PhotoUploadResult> AddPhoto(IFormFile file)
            {
                  if(file.Length > 0)
                  {
                        await using var stream = file.OpenReadStream();
                        var uploadParams = new ImageUploadParams
                        {
                              File = new FileDescription(file.FileName, stream),
                              Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                        };

                        var uploadResult = await _cloudianry.UploadAsync(uploadParams);

                        if(uploadResult.Error != null){
                              throw new Exception(uploadResult.Error.Message);
                        }

                        return new PhotoUploadResult
                        {
                              PublicId = uploadResult.PublicId,
                              Url = uploadResult.SecureUrl.ToString()
                        };
                  }
                  return null;
            }

            public async Task<string> DeletePhoto(string publicId)
            {
                  var deleteParams = new DeletionParams(publicId);
                  var result = await _cloudianry.DestroyAsync(deleteParams);
                  return result.Result == "ok" ? result.Result : null;
            }
      }
}