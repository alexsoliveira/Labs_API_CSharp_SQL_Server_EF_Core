using ProductApi.Application.Products;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Products.Application;

public sealed class ListProductsUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldMapAllProducts()
    {
        var repository = new InMemoryProductRepository([
            new Product("Mouse", 120m, 5, "Gaming mouse"),
            new Product("Keyboard", 250m, 2, "Mechanical keyboard")]);
        var useCase = new ListProductsUseCase(repository);

        var result = await useCase.ExecuteAsync(CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, item => item.Name == "Mouse");
        Assert.Contains(result, item => item.Name == "Keyboard");
    }

    private sealed class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public InMemoryProductRepository(IEnumerable<Product> products)
        {
            _products = products.ToList();
        }

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
            return Task.FromResult<IReadOnlyCollection<Product>>(_products.ToArray());
        }
    }
}