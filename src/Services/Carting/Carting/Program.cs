using OnlineStore.CartingService.Configurations.Extensions;
using OnlineStore.CartingService.Configurations.Swagger;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options => MiddlewareConfigurationActions.ConfigureSwaggerMiddleware(app, options));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
