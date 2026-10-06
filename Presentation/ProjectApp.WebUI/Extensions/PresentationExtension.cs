using ProjectApp.WebUI.Services.CategoryServices;

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




            return services;


        }
    }
}
