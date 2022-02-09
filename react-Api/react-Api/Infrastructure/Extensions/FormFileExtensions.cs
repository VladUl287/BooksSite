using System.IO;
using Microsoft.AspNetCore.Http;

namespace api.Infrastructure.Extensions
{
    public static class FormFileExtensions
    {
        public const int ImageMinimumBytes = 512;

        public static bool IsValid(this IFormFile formFile)
        {
            if (formFile is null)
            {
                return false;
            }

            var type = formFile.ContentType.ToLower();
            if (type != "image/jpg" && type != "image/jpeg" && type != "image/pjpeg"
                && type != "image/x-png" && type != "image/png")
            {
                return false;
            }

            var extension = Path.GetExtension(formFile.FileName).ToLower();
            if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
            {
                return false;
            }

            if (formFile.Length < ImageMinimumBytes)
            {
                return false;
            }

            try
            {
                if (!formFile.OpenReadStream().CanRead)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
