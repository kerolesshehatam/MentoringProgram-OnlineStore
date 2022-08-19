using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("ocelot.json");
//builder.Services.AddRazorPages();

//builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
//           .AddIdentityServerAuthentication(options =>
//           {
//               options.ApiName = "catalogApi";
//               options.Authority = builder.Configuration.GetValue<string>("IdentityUrlExternal");
//               options.RequireHttpsMetadata = false;
//           });
builder.Services.AddControllers();

builder.Services.AddOcelot();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseOcelot().Wait();
app.UseAuthorization();

//app.MapRazorPages();

app.Run();

