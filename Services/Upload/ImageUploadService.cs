using Microsoft.AspNetCore.Http;

namespace Services.Upload
{
    public static class ImageUploadService
    {
        public static string? Upload(IFormFile formFile, string? photoPath = null)
        {
            if (formFile != null && photoPath != null)
            {
                try
                {
                    var imagePath = Path.Combine("wwwroot/Uploads/", photoPath);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);

                    }
                }
                catch { }

            }

            if (formFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                string extension = Path.GetExtension(formFile.FileName);
                photoPath = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine("wwwroot/Uploads/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

            }

            return photoPath;
        }
    }
}
