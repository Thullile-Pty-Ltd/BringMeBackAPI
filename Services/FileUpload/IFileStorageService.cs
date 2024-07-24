namespace BringMeBackAPI.Services.FileUpload
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }

}
