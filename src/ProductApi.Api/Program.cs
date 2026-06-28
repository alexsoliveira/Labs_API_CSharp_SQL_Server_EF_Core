using ProductApi.Application.Products;
using ProductApi.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<ListProductsUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<SearchProductsByNameUseCase>();
builder.Services.AddScoped<GetPagedProductsUseCase>();

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=localhost;Database=ProductApi;User Id=sa;Password=Your_password123;TrustServerCertificate=True");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    var executionStrategy = dbContext.Database.CreateExecutionStrategy();

    await executionStrategy.ExecuteAsync(async () =>
    {
        await dbContext.Database.MigrateAsync();
    });
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();


app.Run();

public partial class Program;
