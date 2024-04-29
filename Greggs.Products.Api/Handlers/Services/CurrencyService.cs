using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Handlers.Services;

public record ConversionResult(bool Success, decimal Amount);
public class CurrencyService : ICurrencyService
{
    List<CurrencyPair> _currencyPairs = new List<CurrencyPair>
    {
        new CurrencyPair {From = "GBP", To = "EUR", Rate = 1.11m},

    };
    public ConversionResult Convert(decimal amount, string currency)
    {
        if(currency == "GBP")
        {
            return new ConversionResult(true, amount);
        }
        
        
        var currencyPair = _currencyPairs
            .FirstOrDefault(x => x.From == "GBP" && x.To == currency);
        
        
        if (currencyPair == null)
        {
            return new ConversionResult(false, 0);
        }
        
        var convertedAmount = Math.Round( amount * currencyPair.Rate, 2);
        
        
        return new ConversionResult( true,   convertedAmount ) ;
    }
}