using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Handlers;

public class GetProducts : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private readonly IDataAccess<Product> _productsDataAccess;
    private readonly ILogger<GetProducts> _logger;

    public GetProducts(IDataAccess<Product> productsDataAccess, ILogger<GetProducts> logger)
    {
        _productsDataAccess = productsDataAccess;
        _logger = logger;
    }
    public Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
         

        var products =  _productsDataAccess.List(request.Start, request.PageSize)
            .ToArray();
        
        _logger.LogInformation("Retrieved {ProductCount} products", products.Length);
        
        return Task.FromResult(new GetProductsResponse(products));
        

    }
}

public record GetProductsResponse(Product[] Products);