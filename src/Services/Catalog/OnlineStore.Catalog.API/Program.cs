using Autofac.Extensions.DependencyInjection;
using OnlineStore.Catalog.API.Configurations.Extensions;
using OnlineStore.Catalog.API.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.RegisterServices(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options => MiddlewareConfigurationActions.ConfigureSwaggerMiddleware(app, options));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    public static string AppName = "CatalogService";
}
