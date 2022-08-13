using Autofac.Extensions.DependencyInjection;
using OnlineStore.CartingService.Configurations.Extensions;
using OnlineStore.CartingService.Configurations.Swagger;


WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    MiddlewareConfigurationActions.ConfigureSwaggerMiddleware(app, options);
    options.OAuthClientId("cartingserviceswaggerui");
    options.OAuthAppName("CartingService Swagger UI");
});

app.ConfigureEventBus();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    public static string AppName = "CartingService";
}


