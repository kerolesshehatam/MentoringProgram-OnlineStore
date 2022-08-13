using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace OnlineStore.Catalog.API.Configurations.Registrars;

public class MvcRegistrar : IRegistrar
{
    public void RegisterServices(WebApplicationBuilder builder)
    {

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        //    options.ClientId = "catalogserviceswaggerui";
        //    options.ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A";
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        //})
        builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
              {
                  options.ApiName = "catalogApi";
                  options.Authority = builder.Configuration.GetValue<string>("IdentityUrlExternal");
                  options.RequireHttpsMetadata = false;
              });
        //    .AddJwtBearer(options =>
        //{
        //    options.Authority = builder.Configuration.GetValue<string>("IdentityUrlExternal"); ;
        //    options.RequireHttpsMetadata = false;
        //    options.Audience = "catalogApi";
        //});

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        builder.Services.AddControllers()
                        .AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter()))
                        .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
