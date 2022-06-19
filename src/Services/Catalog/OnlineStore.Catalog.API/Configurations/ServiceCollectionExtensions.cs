using Microsoft.EntityFrameworkCore;
using OnlineStore.Catalog.Persistence;

namespace OnlineStore.Catalog.API.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogContext>(options =>
                {
                    options.UseSqlServer(configuration["ConnectionString"]);
                });

            return services;
        }

    }
}
