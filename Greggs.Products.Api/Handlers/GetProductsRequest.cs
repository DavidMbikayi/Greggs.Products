using MediatR;

namespace Greggs.Products.Api.Handlers;

public class GetProductsRequest : IRequest<GetProductsResponse>
{
    public int Start { get; set; }
    
    public int PageSize { get; set; }
    
    public string Currency { get; set; }
}