using ProjectApp.WebUI.Base;
using System.Net.Http.Headers;

namespace ProjectApp.WebUI.Services.FileServices
{
    public class FileService(HttpClient _client) : IFileService
    {
        // POST api/files/Product → Data = "/Images/Product/abc.jpg"
        public async Task<ApiResult<string>> UploadAsync(IFormFile file, string folder)
        {
            using var form = new MultipartFormDataContent();

            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            form.Add(fileContent, "file", file.FileName); // "file" → FilesController'daki parametre adı

            var response = await _client.PostAsync($"files/{folder}", form);
            return await response.Content.ReadFromJsonAsync<ApiResult<string>>() ?? new();
        }

        // DELETE api/files?fileUrl=/Images/Product/abc.jpg
        public async Task DeleteAsync(string fileUrl)
        {
            // EscapeDataString → yoldaki "/" gibi karakterler URL'yi bozmasın
            await _client.DeleteAsync($"files?fileUrl={Uri.EscapeDataString(fileUrl)}");
        }
    }
}