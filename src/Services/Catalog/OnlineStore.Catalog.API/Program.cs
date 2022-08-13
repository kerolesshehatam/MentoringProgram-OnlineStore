using Autofac.Extensions.DependencyInjection;
using OnlineStore.Catalog.API.Configurations.Extensions;
using OnlineStore.Catalog.API.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.RegisterServices(typeof(Program));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger()
   .UseSwaggerUI(options =>
    {
        MiddlewareConfigurationActions.ConfigureSwaggerMiddleware(app, options);
        options.OAuthClientId("catalogserviceswaggerui");
        options.OAuthAppName("CatalogService Swagger UI");
        options.OAuthUsePkce();
    });

app.UseCors("DefaultCorsPolicy");

app.MapControllers();

app.Run();

public partial class Program
{
    public static string AppName = "CatalogService";
}
