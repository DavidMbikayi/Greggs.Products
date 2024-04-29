namespace Greggs.Products.Api.Handlers.Services;

public interface ICurrencyService
{
    ConversionResult ConvertToEuros(double amount, string currency);
}