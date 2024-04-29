using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FakeItEasy;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Handlers;
using Greggs.Products.Api.Handlers.Services;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Greggs.Products.UnitTests;

public class RequestHandlerUnitTests
{
    
    IFixture _fixture;

    public RequestHandlerUnitTests()
    {
        _fixture = new Fixture();
    }
    
    [Fact]
    public async Task GetProductsRequestHandler_ShouldReturnProducts()
    {
        // Arrange
        var request = _fixture.Create<GetProductsRequest>();
        var products = _fixture.CreateMany<Product>().ToList();
        var currencyService = _fixture.Create<Fake<ICurrencyService>>();
        var productRepository = _fixture.Create<Fake<IDataAccess<Product>>>();
        var logger = _fixture.Create<Fake<ILogger<GetProducts>>>();
        var sut = new GetProducts(productRepository.FakedObject, logger.FakedObject);
        
        productRepository.CallsTo( x => x.List(request.Start, request.PageSize)).Returns(products);
        
        
        // Act
        var result = await sut.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Products.Length);
    }
}