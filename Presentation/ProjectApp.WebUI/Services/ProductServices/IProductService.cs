using ProjectApp.WebUI.Base;
using ProjectApp.WebUI.DTOs.ProductDtos;

namespace ProjectApp.WebUI.Services.ProductServices
{
    public interface IProductService
    {

        Task<ApiResult<List<GetProductsWithCategoryQueryResult>>> GetAllProductWithCategoryAsync();
        Task<ApiResult<GetProductByIdQueryResult>> GetByIdAsync(int id);

        Task<ApiResult<object>> CreateAsync(CreateProductCommand command);
        Task<ApiResult<object>> UpdateAsync(UpdateProductCommand command);
        Task<ApiResult<object>> DeleteAsync(int id);






    }
}
