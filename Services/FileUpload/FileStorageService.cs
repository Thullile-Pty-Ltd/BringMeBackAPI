namespace BringMeBackAPI.Services.FileUpload
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _storagePath;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _storagePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(_storagePath);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }
    }

}
