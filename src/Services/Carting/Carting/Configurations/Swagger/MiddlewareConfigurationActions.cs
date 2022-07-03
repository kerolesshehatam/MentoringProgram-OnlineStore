using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OnlineStore.CartingService.Configurations.Swagger
{
    public class MiddlewareConfigurationActions
    {
        public static SwaggerUIOptions ConfigureSwaggerMiddleware(WebApplication app, SwaggerUIOptions options)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.ApiVersion.ToString()
                    );
            }

            return options;
        }
    }
}
