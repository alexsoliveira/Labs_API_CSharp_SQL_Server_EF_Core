using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Domain.Products;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptions =>
                sqlServerOptions.EnableRetryOnFailure()));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}