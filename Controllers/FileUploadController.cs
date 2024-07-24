using BringMeBackAPI.Services.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace BringMeBackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;

        public FileUploadController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        [HttpPost("fileupload")]
        public async Task<IActionResult> UploadFiles([FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest("No files uploaded.");
            }

            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileUrl = await _fileStorageService.SaveFileAsync(file);
                    fileUrls.Add(fileUrl);
                }
            }

            return Ok(new { FileUrls = fileUrls });
        }
    }

}
