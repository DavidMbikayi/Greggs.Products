using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Handlers;
using Greggs.Products.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    private static readonly string[] Products = new[]
    {
        "Sausage Roll", "Vegan Sausage Roll", "Steak Bake", "Yum Yum", "Pink Jammie"
    };

    private readonly ILogger<ProductController> _logger;


    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
        
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get(int pageStart = 0, int pageSize = 5, string currency = "GBP")
    {
        var request = new GetProductsRequest
        {
            Start = pageStart,
            PageSize = pageSize,
            Currency = currency
        };
        
        var response = await _mediator.Send(request);
        return response.Products;
    }
}