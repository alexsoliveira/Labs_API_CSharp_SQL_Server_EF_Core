using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class CreateProductUseCase
{
    private readonly IProductRepository _repository;

    public CreateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price, request.Stock, request.Description);

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}