using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class GetPagedProductsUseCase
{
    private readonly IProductRepository _repository;

    public GetPagedProductsUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProductResponse>> ExecuteAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        var normalizedPage = page < 1 ? 1 : page;
        var normalizedPageSize = pageSize < 1 ? 10 : pageSize;

        var items = products
            .Skip((normalizedPage - 1) * normalizedPageSize)
            .Take(normalizedPageSize)
            .Select(product => new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Stock,
                product.CreatedAt,
                product.UpdatedAt))
            .ToArray();

        return new PagedResult<ProductResponse>(items, normalizedPage, normalizedPageSize, products.Count);
    }
}