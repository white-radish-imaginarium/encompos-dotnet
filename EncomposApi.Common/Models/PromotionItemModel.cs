using System;
using System.ComponentModel.DataAnnotations;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public class PromotionItemModel
    {
        [Required]
        public decimal Id { get; set; }
        public string ProductCode { get; set; }

        public long PromotionId { get; set; }

        public Optional<PromotionModel> Promotion { get; set; }

        public Optional<long> DefinitionId { get; set; }
        public Optional<string> DefinitionName { get; set; }
        public Optional<long> PriceLevel { get; set; }
        public Optional<decimal> NewPrice { get; set; }
        public Optional<decimal> BuyX { get; set; }
        public Optional<decimal> GetY { get; set; }
        public Optional<decimal> PercentOff { get; set; }
        public Optional<decimal> AmountOff { get; set; }

        public Optional<decimal> MarkupFromCost { get; set; }
        public Optional<decimal> Margin { get; set; }
        public Optional<decimal> DealCost { get; set; }
        public Optional<decimal> DealPercent { get; set; }
        public Optional<string> DealIndicator { get; set; }
    }
}
