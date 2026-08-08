using System;

namespace EncomposApi.Models;

public record SalesLineModel
{
    public string ProductCode { get; init; }

    public DateTime TranDate { get; init; }

    public string LineCode { get; init; }

    public decimal? Qty { get; init; }

    public decimal? PriceAtos { get; init; }

    public decimal? CostAtos { get; init; }

    public decimal? DiscountTimeOfSale1 { get; init; }

    public decimal? DiscountTimeOfSale2 { get; init; }

    public decimal? DiscountMember { get; init; }

    public decimal? DiscountStoreCoupon { get; init; }

    public decimal? DiscountAccumulatedAward { get; init; }

    public decimal? DiscountAssembly { get; init; }

    public decimal? DiscountPromotions { get; init; }
}
