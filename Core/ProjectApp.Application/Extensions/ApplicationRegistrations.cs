using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

            services.AddValidatorsFromAssembly(typeof(ApplicationAssembly).Assembly);
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }


    }
}
