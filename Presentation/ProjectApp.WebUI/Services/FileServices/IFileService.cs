using ProjectApp.WebUI.Base;

namespace ProjectApp.WebUI.Services.FileServices
{
    public interface IFileService
    {
        Task<ApiResult<string>> UploadAsync(IFormFile file, string folder);
        Task DeleteAsync(string fileUrl);
    }
}