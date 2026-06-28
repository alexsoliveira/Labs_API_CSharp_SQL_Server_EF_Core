using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class UpdateProductUseCase
{
    private readonly IProductRepository _repository;

    public UpdateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return false;
        }

        product.Update(request.Name, request.Price, request.Stock, request.Description);

        await _repository.UpdateAsync(product, cancellationToken);

        return true;
    }
}