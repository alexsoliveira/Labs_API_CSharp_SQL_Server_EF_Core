using ProductApi.Application.Products;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Products.Application;

public sealed class UpdateProductUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldUpdateProduct_WhenItExists()
    {
        var product = new Product("Mouse", 120m, 5, "Gaming mouse");
        var repository = new InMemoryProductRepository([product]);
        var useCase = new UpdateProductUseCase(repository);

        var result = await useCase.ExecuteAsync(product.Id, new UpdateProductRequest("Keyboard", "Mechanical", 250m, 2), CancellationToken.None);

        Assert.True(result);
        Assert.Equal("Keyboard", product.Name);
        Assert.Equal(250m, product.Price);
        Assert.Equal(2, product.Stock);
        Assert.Equal("Mechanical", product.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
    {
        var repository = new InMemoryProductRepository([]);
        var useCase = new UpdateProductUseCase(repository);

        var result = await useCase.ExecuteAsync(Guid.NewGuid(), new UpdateProductRequest("Keyboard", null, 250m, 2), CancellationToken.None);

        Assert.False(result);
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