using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.CartingService.Configurations.Registrars
{
    public class MvcRegistrar : IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
