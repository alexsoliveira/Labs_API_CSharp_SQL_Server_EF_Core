using ProductApi.Application.Products;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Products.Application;

public sealed class GetProductByIdUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnProduct_WhenItExists()
    {
        var product = new Product("Mouse", 120m, 5, "Gaming mouse");
        var repository = new InMemoryProductRepository([product]);
        var useCase = new GetProductByIdUseCase(repository);

        var result = await useCase.ExecuteAsync(product.Id, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(product.Id, result!.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnNull_WhenProductDoesNotExist()
    {
        var repository = new InMemoryProductRepository([]);
        var useCase = new GetProductByIdUseCase(repository);

        var result = await useCase.ExecuteAsync(Guid.NewGuid(), CancellationToken.None);

        Assert.Null(result);
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