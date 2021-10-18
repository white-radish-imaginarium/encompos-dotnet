using System;
using EncomposApi.Enums;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public class PriceChangeModel
    {
        public decimal Id { get; set; }
        public Optional<string> ProductCode { get; set; }
        public Optional<DateTimeOffset> DateChanged { get; set; }
        public Optional<decimal> OldCost { get; set; }
        public Optional<decimal> NewCost { get; set; }
        public Optional<decimal> OldRetail { get; set; }
        public Optional<decimal> NewRetail { get; set; }
        public Optional<decimal> OldMargin { get; set; }
        public Optional<decimal> NewMargin { get; set; }
        public Optional<PriceChangeReason> Reason { get; set; }
        public Optional<int> CompetitorId { get; set; }
    }
}
