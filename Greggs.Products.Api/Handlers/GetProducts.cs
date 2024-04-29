using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Handlers.Services;
using Greggs.Products.Api.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Handlers;

public class GetProducts : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private readonly IDataAccess<Product> _productsDataAccess;
    private readonly ICurrencyService _currencyService;
    private readonly ILogger<GetProducts> _logger;

    public GetProducts(IDataAccess<Product> productsDataAccess, ICurrencyService currencyService, ILogger<GetProducts> logger)
    {
        _productsDataAccess = productsDataAccess;
        _currencyService = currencyService;
        _logger = logger;
    }
    public Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
         

        var products =  _productsDataAccess.List(request.Start, request.PageSize)
            .Select(x => new Product
            {
                Name = x.Name,
                //This should be improved :(
                Price =  _currencyService.Convert(x.Price, request.Currency).Amount, 
                
                Currency = request.Currency
                
            })
            .ToArray();
        
          if(products.All(x => x.Price == 0))
          {
              _logger.LogError("Currency conversion failed for all products. Returning empty list.");
              return Task.FromResult(new GetProductsResponse(Array.Empty<Product>()));
          }
          
          
        
        _logger.LogInformation("Retrieved {ProductCount} products", products.Length);
        
        
        return Task.FromResult(new GetProductsResponse(products));
        

    }
}

public record GetProductsResponse(Product[] Products);