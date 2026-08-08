using System;
using EncomposApi.Models;
using Newtonsoft.Json;
using Xunit;

namespace EncomposApi.Common.Tests;

public class SalesQueryTests : TestBase
{
    [Fact]
    public void Validator_AcceptsInclusiveSingleDayRange()
    {
        var date = new DateTime(2026, 7, 1);
        var query = new SalesQuery
        {
            ProductCodes = ["12345"],
            FromDate = date,
            ToDate = date
        };

        var result = new SalesQueryValidator().Validate(query);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validator_RejectsMissingProductsAndReversedRange()
    {
        var query = new SalesQuery
        {
            ProductCodes = [],
            FromDate = new DateTime(2026, 7, 2),
            ToDate = new DateTime(2026, 7, 1)
        };

        var result = new SalesQueryValidator().Validate(query);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(SalesQuery.ProductCodes));
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(SalesQuery.ToDate));
    }

    [Fact]
    public void Validator_RejectsNullProductCodes()
    {
        var query = new SalesQuery
        {
            ProductCodes = null,
            FromDate = new DateTime(2026, 7, 1),
            ToDate = new DateTime(2026, 7, 1)
        };

        var result = new SalesQueryValidator().Validate(query);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.PropertyName == nameof(SalesQuery.ProductCodes));
    }

    [Fact]
    public void SalesLine_SerializesFixedSalesAndDiscountFields()
    {
        var line = new SalesLineModel
        {
            ProductCode = "12345",
            TranDate = new DateTime(2026, 7, 1),
            LineCode = "PP",
            Qty = 2,
            PriceAtos = 4.50m,
            CostAtos = 3m,
            DiscountTimeOfSale1 = 0.25m,
            DiscountTimeOfSale2 = 0m,
            DiscountMember = 0.50m,
            DiscountStoreCoupon = 0m,
            DiscountAccumulatedAward = 0m,
            DiscountAssembly = 0m,
            DiscountPromotions = 0m
        };

        var json = JsonConvert.SerializeObject(line);

        Assert.Contains("\"productCode\": \"12345\"", json);
        Assert.Contains("\"lineCode\": \"PP\"", json);
        Assert.Contains("\"priceAtos\": 4.50", json);
        Assert.Contains("\"discountTimeOfSale1\": 0.25", json);
        Assert.Contains("\"discountMember\": 0.50", json);
        Assert.Contains("\"discountPromotions\": 0.0", json);
    }
}
