using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

namespace FileUploadAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResearchController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ResearchController> _logger;

        public ResearchController(ILogger<ResearchController> logger)
        {
            _logger = logger;
        }

        [HttpPost("AddDocument")]
        [Consumes("multipart/form-data")]
        //[DisableRequestSizeLimit,
        //RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue,
        //ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> AddDocumentAsync(IFormFile file)
        {
            try
            {
                return Ok(await UploadStream(file));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred during server request: {ex.Message} - {ex.GetType()}");
            }
        }

        private async Task<bool> UploadStream(IFormFile file)
        {
            Stream stream = file.OpenReadStream();
            using (var fileStream = new FileStream($"C:\\Interview\\{file.FileName}", FileMode.Create, FileAccess.Write))
            {
               await stream.CopyToAsync(fileStream);
            }
            return true;
        }

    }
}