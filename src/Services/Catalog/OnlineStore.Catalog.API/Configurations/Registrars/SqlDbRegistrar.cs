using Microsoft.EntityFrameworkCore;
using OnlineStore.Catalog.Persistence;

namespace OnlineStore.Catalog.API.Configurations.Registrars
{
    public class SqlDbRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<CatalogContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

        }
    }
}
