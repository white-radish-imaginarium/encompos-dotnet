using System;
using EncomposApi.Enums;

namespace EncomposApi.Models
{
    public record QohMovementModel
    {
        public long Id { get; init; }
        public string ProductCode { get; init; }
        public DateTimeOffset Date { get; init; }
        public MovementType Type { get; init; }
        public MovementReason? Reason { get; init; }
        public decimal? Qty { get; init; }
        public decimal? Cost { get; init; }
        public decimal? Retail { get; init; }
    }
}
