using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Common
{
    public class ReflectionHelper
    {
        public static Boolean IsImage(IFormFile image)
        {
            if (image.ContentType.ToLower() != "image/jpg" &&
                image.ContentType.ToLower() != "image/jpeg" &&
                image.ContentType.ToLower() != "image/gif" &&
                image.ContentType.ToLower() != "image/x-png" &&
                image.ContentType.ToLower() != "image/png")
            {
                return false;
            }
            if (Path.GetExtension(image.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(image.FileName).ToLower() != ".pjpeg" &&
                Path.GetExtension(image.FileName).ToLower() != ".gif" &&
                Path.GetExtension(image.FileName).ToLower() != ".png")
            {
                return false;
            }
            return true;
        }
        public static int RandomString(int number)
        {
            return (new Random()).Next(number);
        }
    }
}
