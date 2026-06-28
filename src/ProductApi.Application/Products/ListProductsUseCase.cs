using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class ListProductsUseCase
{
    private readonly IProductRepository _repository;

    public ListProductsUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyCollection<ProductResponse>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

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