namespace ProductApi.Application.Products;

public sealed record UpdateProductRequest(
    string Name,
    string? Description,
    decimal Price,
    int Stock);