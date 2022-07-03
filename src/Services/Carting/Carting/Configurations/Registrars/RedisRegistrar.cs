using Microsoft.Extensions.Options;
using OnlineStore.CartingService.Infrastructure.Redis;
using StackExchange.Redis;

namespace OnlineStore.CartingService.Configurations.Registrars
{
    public class RedisRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            builder.Services.AddDistributedMemoryCache();
            builder.Services.Configure<CartSettings>(builder.Configuration);

            //By connecting here we are making sure that our service
            //cannot start until redis is ready. This might slow down startup,
            //but given that there is a delay on resolving the ip address
            //and then creating the connection it seems reasonable to move
            //that cost to startup instead of having the first request pay the
            //penalty.
            builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<CartSettings>>().Value;
                var configuration = ConfigurationOptions.Parse(settings.ConnectionString, true);

                return ConnectionMultiplexer.Connect(configuration);
            });
        }
    }
}
