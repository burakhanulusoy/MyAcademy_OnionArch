using Microsoft.AspNetCore.Mvc;
using ProjectApp.Application.Base;
using ProjectApp.Application.Services.FileServices;

namespace ProjectApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController(IFileService _fileService) : ControllerBase
    {
        private static readonly string[] _allowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
        private static readonly string[] _allowedFolders = ["Product", "Banner"];
        private const long MaxFileSize = 2 * 1024 * 1024; // 2 MB

        // POST api/files/Product  veya  POST api/files/Banner
        [HttpPost("{folder}")]
        public async Task<IActionResult> Upload(string folder, IFormFile? file)
        {
            // Sadece izin verdiğimiz klasörlere yüklensin (ör. "../" gibi tehlikeli yollar engellensin)
            if (!_allowedFolders.Contains(folder))
                return BadRequest(BaseResult<string>.Fail("Geçersiz klasör."));

            if (file is null || file.Length == 0)
                return BadRequest(BaseResult<string>.Fail("Görsel seçilmelidir."));

            if (file.Length > MaxFileSize)
                return BadRequest(BaseResult<string>.Fail("Görsel en fazla 2 MB olabilir."));

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return BadRequest(BaseResult<string>.Fail("Sadece jpg, jpeg, png veya webp yüklenebilir."));

            var imageUrl = await _fileService.UploadAsync(file, folder);

            // Data = "/Images/Product/abc.jpg"
            return Ok(BaseResult<string>.Success(imageUrl));
        }

        // DELETE api/files?fileUrl=/Images/Product/abc.jpg
        // WebUI, görseli yükleyip ürünü kaydedemezse boşta kalan görseli bununla siler
        [HttpDelete]
        public IActionResult Delete([FromQuery] string fileUrl)
        {
            // Sadece Images klasöründeki dosyalar silinebilsin
            // (".." ile üst klasörlere çıkıp appsettings.json gibi dosyaları silmeyi engeller)
            if (!fileUrl.StartsWith("/Images/") || fileUrl.Contains(".."))
                return BadRequest(BaseResult<object>.Fail("Geçersiz dosya yolu."));

            _fileService.Delete(fileUrl);
            return Ok(BaseResult<object>.Success());
        }
    }
}