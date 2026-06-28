namespace ProductApi.Domain.Products;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken);

    Task UpdateAsync(Product product, CancellationToken cancellationToken);

    Task DeleteAsync(Product product, CancellationToken cancellationToken);

    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Product>> SearchByNameAsync(string name, CancellationToken cancellationToken);
}