using OnlineStore.Catalog.Application.Infrastructure;
using OnlineStore.Catalog.Application.Services;
using OnlineStore.Catalog.Persistence.Repositories;

namespace OnlineStore.Catalog.API.Configurations.Registrars
{
    public class ServicesRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            builder.Services
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<ICategoryItemRepository, CategoryItemRepository>()
                .AddScoped<ICatalogService, CatalogService>();

        }
    }
}
