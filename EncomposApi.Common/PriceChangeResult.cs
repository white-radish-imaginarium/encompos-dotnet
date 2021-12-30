using EncomposApi.Models;

namespace EncomposApi
{
    public record PriceChangeResult
    {
        public PriceChangeModel[] PriceChanges { get; init; }
    }
}
