using ProjectApp.WebUI.Services.CategoryServices;
using ProjectApp.WebUI.Services.FileServices;
using ProjectApp.WebUI.Services.ProductServices;

namespace ProjectApp.WebUI.Extensions
{
    public static class PresentationExtension
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services,IConfiguration _configuration)
        {

            services.AddHttpClient<ICategoryService, CategoryService>(client =>
            {
                client.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
            });

            services.AddHttpClient<IProductService, ProductService>(client =>
            {
                client.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
            });

            services.AddHttpClient<IFileService, FileService>(client =>
            {
                client.BaseAddress = new Uri(_configuration["ApiBaseUrl"]!);
            });
            return services;


        }
    }
}
