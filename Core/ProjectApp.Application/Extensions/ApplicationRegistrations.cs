using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProjectApp.Application.Services.FileServices;

namespace ProjectApp.Application.Extensions
{
    public static class ApplicationRegistrations
    {

        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(options =>
            {

                options.RegisterServicesFromAssemblies(typeof(ApplicationAssembly).Assembly);

            });

            services.AddScoped<IFileService, FileService>();

            services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }


    }
}
