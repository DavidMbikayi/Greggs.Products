using Greggs.Products.Api.Handlers.Services;
using Xunit;

namespace Greggs.Products.UnitTests;

public class CurrencyServiceTests
{
    
    [Theory]
    [InlineData(100, "EUR", true, 110.00)]
    [InlineData(100, "USD", false, 0)]
    [InlineData(0, "EUR", true, 0)]
    public void Convert_to_euros(double amount, string currency, bool sucess, double expectedAmount)
    {
        // Arrange
        var currencyService = new CurrencyService();

        // Act
        var result = currencyService.ConvertToEuros(amount, currency);

        // Assert
        Assert.Equal(sucess, result.Success);
        Assert.Equal(expectedAmount, result.Amount);
    }
    
}