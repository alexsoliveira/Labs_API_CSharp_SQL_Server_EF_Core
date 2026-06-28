using ProductApi.Application.Products;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Products.Application;

public sealed class CreateProductUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldPersistProduct_AndReturnIdentifier()
    {
        var repository = new InMemoryProductRepository();
        var useCase = new CreateProductUseCase(repository);
        var request = new CreateProductRequest("Mouse", "Gaming mouse", 120m, 5);

        var productId = await useCase.ExecuteAsync(request, CancellationToken.None);

        Assert.Single(repository.Products);
        Assert.Equal(productId, repository.Products[0].Id);
        Assert.Equal("Mouse", repository.Products[0].Name);
    }

    private sealed class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = [];

        public IReadOnlyList<Product> Products => _products;

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