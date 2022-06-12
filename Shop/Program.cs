using GraphQL.SystemTextJson;
using GraphQL.Server;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shop.API.GraphQL.Schemas;
using Shop.Data;
using Shop.Data.IRepositories;
using Shop.Data.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ShopContext>(
    db => db.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();

builder.Services.AddScoped<ISchema, AutoSchema>();
builder.Services.AddGraphQL(options => { options.EnableMetrics = true; }).AddSystemTextJson();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Shop",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API",
        Contact = new OpenApiContact
        {
            Name = "Andrey Antonov",
            Email = "camuy4onkoa4a@yandex.ru",
        }
    });
});



var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
SeedDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseGraphQL<ISchema>();

app.MapControllers();

app.Run();


void SeedDatabase() 
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            SeedData.Initialize(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}
