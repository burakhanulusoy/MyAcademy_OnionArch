using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectApp.Application.Contracts;
using ProjectApp.Persistence.Concrete;
using ProjectApp.Persistence.Context;

namespace ProjectApp.Persistence.Extensions
{
    public static class PersistenceRegistrations
    {
        public static IServiceCollection  AddPersistanceServices(this IServiceCollection services,IConfiguration _configuration)
        {
         
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("MSSQLConnection"));
                options.UseLazyLoadingProxies();

            });

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }


    }
}
