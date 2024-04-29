namespace Greggs.Products.Api.Handlers.Services;

public interface ICurrencyService
{
    ConversionResult Convert(decimal amount, string currency);
}