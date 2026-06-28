using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Api;

public sealed class ProductApiFactory : WebApplicationFactory<Program>
{
    private readonly InMemoryProductRepository _repository = new();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IProductRepository>();
            services.AddSingleton<IProductRepository>(_repository);
        });
    }
}

public sealed class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = [];

    public Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Product product, CancellationToken cancellationToken)
    {
        _products.Remove(product);
        return Task.CompletedTask;
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult<Product?>(_products.FirstOrDefault(product => product.Id == id));
    }

    public Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Product>>(_products.ToArray());
    }

    public Task<IReadOnlyCollection<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Product>>(
            _products.Where(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToArray());
    }
}