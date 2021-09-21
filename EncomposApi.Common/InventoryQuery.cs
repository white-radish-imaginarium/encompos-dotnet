using FluentValidation;

namespace EncomposApi
{
    public record InventoryQuery
    {
        public string[] Codes { get; init; }
        
        /// <summary>
        /// Include catalog items in the results.
        /// </summary>
        public bool IncludeCatalogs { get; init; }
        
        /// <summary>
        /// Ignore aliases when looking up inventory.
        /// </summary>
        public bool IgnoreAliases { get; init; }

        public bool IncludeOpenOrders { get; init; }

        public bool IncludePromotions { get; init; }

        public bool QueryFixedPrices { get; init; }

        public bool IncludeRecentSales { get; init; }
    }

    public class InventoryQueryValidator : AbstractValidator<InventoryQuery>
    {
        public InventoryQueryValidator()
        {
            RuleFor(p => p.Codes).Must(codes => codes.Length <= 100).When(p => p.Codes is not null).WithMessage("Too many codes");
        }
    }
}
