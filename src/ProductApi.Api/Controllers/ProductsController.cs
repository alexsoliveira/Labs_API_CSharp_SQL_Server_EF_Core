using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.Products;

namespace ProductApi.Api.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _createProductUseCase;
    private readonly DeleteProductUseCase _deleteProductUseCase;
    private readonly GetProductByIdUseCase _getProductByIdUseCase;
    private readonly GetPagedProductsUseCase _getPagedProductsUseCase;
    private readonly ListProductsUseCase _listProductsUseCase;
    private readonly SearchProductsByNameUseCase _searchProductsByNameUseCase;
    private readonly UpdateProductUseCase _updateProductUseCase;

    public ProductsController(
        CreateProductUseCase createProductUseCase,
        DeleteProductUseCase deleteProductUseCase,
        GetProductByIdUseCase getProductByIdUseCase,
        GetPagedProductsUseCase getPagedProductsUseCase,
        ListProductsUseCase listProductsUseCase,
        SearchProductsByNameUseCase searchProductsByNameUseCase,
        UpdateProductUseCase updateProductUseCase)
    {
        _createProductUseCase = createProductUseCase;
        _deleteProductUseCase = deleteProductUseCase;
        _getProductByIdUseCase = getProductByIdUseCase;
        _getPagedProductsUseCase = getPagedProductsUseCase;
        _listProductsUseCase = listProductsUseCase;
        _searchProductsByNameUseCase = searchProductsByNameUseCase;
        _updateProductUseCase = updateProductUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var productId = await _createProductUseCase.ExecuteAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = productId }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _getProductByIdUseCase.ExecuteAsync(id, cancellationToken);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var products = await _listProductsUseCase.ExecuteAsync(cancellationToken);

        return Ok(products);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var updated = await _updateProductUseCase.ExecuteAsync(id, request, cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _deleteProductUseCase.ExecuteAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name, CancellationToken cancellationToken)
    {
        var products = await _searchProductsByNameUseCase.ExecuteAsync(name, cancellationToken);

        return Ok(products);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var pagedProducts = await _getPagedProductsUseCase.ExecuteAsync(page, pageSize, cancellationToken);

        return Ok(pagedProducts);
    }
}