using EncomposApi.Models;

namespace EncomposApi
{
    public record PriceChangeResult
    {
        public int Remaining { get; init; }
        public PriceChangeQuery NextQuery { get; init; }
        public PriceChangeModel[] PriceChanges { get; init; }
        public PriceChangeReasonModel[] Reasons { get; init; }
    }
}
