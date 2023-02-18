using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Management.Data;
using Management_Common.Common;
using Management_Common.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Management_Core.Services
{
    public interface IImageServices
    {
        Task<string> UploadImage(Guid UserId, IFormFile file);
    }
    public class ImageServices : IImageServices
    {
        public Cloudinary? cloudinary;
        private readonly ManagementDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public ImageServices(ManagementDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<string> UploadImage(Guid UserId, IFormFile file)
        {
            var fileName = DateTime.Now.ToString("ddMMyyyyHHmmssffff") + (new Random()).Next(1000, 99999).ToString() + ReflectionHelper.RandomString(10) + Path.GetExtension(file.FileName);
            var userQuery = await _dbContext.Users.Include(x => x.Images).Where(x => x.Id == UserId).FirstOrDefaultAsync();
            //check exist image
            //if(userQuery != null)
            //{
            //    System.IO.File.Delete(userQuery.Images.SingleOrDefault(x=>x.ImagePath));
            //}
            throw new NotImplementedException();
        }

        private void CloudinaryStorage()
        {
            var cloudName = _configuration["Cloudinary:cloud_name"];
            var apiKey = _configuration["Cloudinary:api_key"];
            var apiSecret = _configuration["Cloudinary:api_secret"];
            var accountCloudinary = new Account(
                cloudName, apiKey, apiSecret);
            cloudinary = new Cloudinary(accountCloudinary);

        }
        private void uploadImage(string imagePath)
        {
            var uploadParams = new ImageUploadParams() { 
                File = new FileDescription(imagePath)
            };
            cloudinary.Upload(uploadParams);

        }
    }
}
