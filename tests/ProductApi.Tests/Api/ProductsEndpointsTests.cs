using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using ProductApi.Application.Products;
using ProductApi.Domain.Products;

namespace ProductApi.Tests.Api;

public sealed class ProductsEndpointsTests
{
    [Fact]
    public async Task Post_Create_ShouldReturnCreatedAndPersistProduct()
    {
        await using var factory = new ProductApiFactory();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/products", new CreateProductRequest("Mouse", "Gaming mouse", 120m, 5));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var listResponse = await client.GetFromJsonAsync<IReadOnlyCollection<ProductResponse>>("/api/products");

        Assert.NotNull(listResponse);
        Assert.Single(listResponse!);
        Assert.Equal("Mouse", listResponse.First().Name);
    }

    [Fact]
    public async Task Post_Create_ShouldReturnProblemDetails_WhenNameIsInvalid()
    {
        await using var factory = new ProductApiFactory();
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/products", new CreateProductRequest(string.Empty, null, 120m, 5));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal(400, problem!.Status);
        Assert.Equal("Invalid request.", problem.Title);
    }
}