using ProductApi.Domain.Products;

namespace ProductApi.Tests.Products;

public sealed class ProductTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenNameIsInvalid()
    {
        Assert.Throws<ArgumentException>(() => new Product(string.Empty, 10m, 1));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenPriceIsNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Product("Mouse", -1m, 1));
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenStockIsNegative()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Product("Mouse", 10m, -1));
    }

    [Fact]
    public void Update_ShouldChangeValues_AndSetUpdatedAt()
    {
        var product = new Product("Mouse", 10m, 1, "Original");

        product.Update("Keyboard", 25m, 3, "Updated");

        Assert.Equal("Keyboard", product.Name);
        Assert.Equal(25m, product.Price);
        Assert.Equal(3, product.Stock);
        Assert.Equal("Updated", product.Description);
        Assert.NotNull(product.UpdatedAt);
    }
}