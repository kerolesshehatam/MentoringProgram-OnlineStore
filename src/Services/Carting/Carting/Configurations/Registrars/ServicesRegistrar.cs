using OnlineStore.CartingService.Infrastructure.Repositories;
using OnlineStore.CartingService.Services;

namespace OnlineStore.CartingService.Configurations.Registrars
{
    public class ServicesRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<ICartRepository, CartRepository>()
                .AddScoped<ICartService, CartService>();
        }
    }
}
