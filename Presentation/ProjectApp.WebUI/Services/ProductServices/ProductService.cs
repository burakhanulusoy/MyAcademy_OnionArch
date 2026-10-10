using ProjectApp.WebUI.Base;
using ProjectApp.WebUI.DTOs.ProductDtos;
using ProjectApp.WebUI.Services.FileServices;

namespace ProjectApp.WebUI.Services.ProductServices
{
    public class ProductService(HttpClient _client, IFileService _fileService) : IProductService
    {
        public async Task<ApiResult<List<GetProductsWithCategoryQueryResult>>> GetAllProductWithCategoryAsync()
        {
            var response = await _client.GetAsync("products");
            return await response.Content.ReadFromJsonAsync<ApiResult<List<GetProductsWithCategoryQueryResult>>>() ?? new();
        }

        public async Task<ApiResult<GetProductByIdQueryResult>> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync($"products/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResult<GetProductByIdQueryResult>>() ?? new();
        }

        public async Task<ApiResult<object>> CreateAsync(CreateProductCommand command)
        {
            // 1. Görsel seçildiyse önce onu yükle
            if (command.Image is not null)
            {
                var upload = await _fileService.UploadAsync(command.Image, "Product");

                if (!upload.IsSuccessful)
                    return ImageError(upload);

                // record değiştirilemez, "with" ile ImageUrl'u dolu yeni bir kopya oluşturuyoruz
                command = command with { ImageUrl = upload.Data };
            }

            // 2. Ürünü JSON olarak gönder
            var response = await _client.PostAsJsonAsync("products", command);
            var result = await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();

            // 3. Ürün kaydedilemediyse az önce yüklenen görsel boşta kalmasın
            if (!result.IsSuccessful && command.ImageUrl is not null)
                await _fileService.DeleteAsync(command.ImageUrl);

            return result;
        }

        public async Task<ApiResult<object>> UpdateAsync(UpdateProductCommand command)
        {
            string? newImageUrl = null;

            // 1. Yeni görsel seçildiyse yükle (seçilmediyse ImageUrl eski görsel olarak kalır)
            if (command.Image is not null)
            {
                var upload = await _fileService.UploadAsync(command.Image, "Product");

                if (!upload.IsSuccessful)
                    return ImageError(upload);

                newImageUrl = upload.Data;
                command = command with { ImageUrl = newImageUrl };
            }

            // 2. Güncellemeyi gönder. Eski görseli SİLMİYORUZ, kayıt başarılıysa API kendisi siliyor
            var response = await _client.PutAsJsonAsync("products", command);
            var result = await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();

            // 3. Güncelleme başarısızsa yeni yüklenen görseli sil, eski görsel yerinde kalır
            if (!result.IsSuccessful && newImageUrl is not null)
                await _fileService.DeleteAsync(newImageUrl);

            return result;
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            // Görseli API siliyor (RemoveProductCommandHandler), burada ayrıca silmiyoruz
            var response = await _client.DeleteAsync($"products/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }

        // Görsel yükleme hatalarını "Image" alanına bağla, formda dosya input'unun altında görünsün
        private static ApiResult<object> ImageError(ApiResult<string> upload)
        {
            return new ApiResult<object>
            {
                Errors = upload.Errors
                    .Select(e => new ApiError { PropertyName = "Image", ErrorMessage = e.ErrorMessage })
                    .ToList()
            };
        }
    }
}