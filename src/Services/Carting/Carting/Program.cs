using Microsoft.Extensions.Options;
using OnlineStore.CartingService.Infrastructure.Redis;
using OnlineStore.CartingService.Infrastructure.Repositories;
using OnlineStore.CartingService.Services;
using StackExchange.Redis;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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


builder.Services
    .AddScoped<ICartRepository, CartRepository>()
    .AddScoped<ICartService, CartService>();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
