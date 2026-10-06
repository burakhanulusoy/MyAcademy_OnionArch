using ProjectApp.WebUI.Base;
using ProjectApp.WebUI.DTOs.CategoryDtos;

namespace ProjectApp.WebUI.Services.CategoryServices
{
    public interface ICategoryService
    {

        Task<ApiResult<List<GetCategoriesQueryResult>>> GetAllAsync();

        Task<ApiResult<GetCategoryByIdQueryResult>> GetByIdAsync(int id);

        Task<ApiResult<object>> CreateAsync(CreateCategoryCommand command);
        Task<ApiResult<object>> UpdateAsync(UpdateCategoryCommand command);
        Task<ApiResult<object>> DeleteAsync(int id);    






    }
}
