namespace ProductApi.Domain.Products;

public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Product(string name, decimal price, int stock, string? description = null)
    {
        Validate(name, price, stock);

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, decimal price, int stock, string? description = null)
    {
        Validate(name, price, stock);

        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void Validate(string name, decimal price, int stock)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (price < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price));
        }

        if (stock < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(stock));
        }
    }
}