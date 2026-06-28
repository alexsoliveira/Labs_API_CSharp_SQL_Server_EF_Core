using ProductApi.Domain.Products;

namespace ProductApi.Application.Products;

public sealed class DeleteProductUseCase
{
    private readonly IProductRepository _repository;

    public DeleteProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return false;
        }

        await _repository.DeleteAsync(product, cancellationToken);

        return true;
    }
}