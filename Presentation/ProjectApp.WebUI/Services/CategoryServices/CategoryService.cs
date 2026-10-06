using ProjectApp.WebUI.Base;
using ProjectApp.WebUI.DTOs.CategoryDtos;

namespace ProjectApp.WebUI.Services.CategoryServices
{
    public class CategoryService(HttpClient _client) : ICategoryService
    {
        public async Task<ApiResult<List<GetCategoriesQueryResult>>> GetAllAsync()
        {
            var response = await _client.GetAsync("categories");
            return await response.Content.ReadFromJsonAsync<ApiResult<List<GetCategoriesQueryResult>>>() ?? new();
        }

        public async Task<ApiResult<GetCategoryByIdQueryResult>> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResult<GetCategoryByIdQueryResult>>() ?? new();
        }

        public async Task<ApiResult<object>> CreateAsync(CreateCategoryCommand command)
        {
            // C# nesnesi → JSON'a çevrilip gönderilir (serialize)
            var response = await _client.PostAsJsonAsync("categories", command);
            // Gelen JSON → ApiResult'a çevrilir (deserialize)
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }

        public async Task<ApiResult<object>> UpdateAsync(UpdateCategoryCommand command)
        {
            var response = await _client.PutAsJsonAsync("categories", command);
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"categories/{id}");
            return await response.Content.ReadFromJsonAsync<ApiResult<object>>() ?? new();
        }
    }
}