using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class SearchProductsByNameUseCase
{
    private readonly IProductRepository _repository;

    public SearchProductsByNameUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ProductResponse>> ExecuteAsync(string name, CancellationToken cancellationToken)
    {
        var products = await _repository.SearchByNameAsync(name, cancellationToken);

        return products
            .Select(product => new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.CreatedAt,
                product.UpdatedAt))
            .ToArray();
    }
}