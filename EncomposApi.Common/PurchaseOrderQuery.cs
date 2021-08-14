using FluentValidation;

namespace EncomposApi
{
    public record PurchaseOrderQuery
    {
        public int? PageSize { get; init; }

        public decimal? BeforePoNumber { get; init; }

        public decimal[] PoNumbers { get; init; }

        public bool IncludeLines { get; init; }
    }

    public class PurchaseOrderQueryValidator : AbstractValidator<PurchaseOrderQuery>
    {
        public PurchaseOrderQueryValidator()
        {
            RuleFor(p => p.PageSize).LessThanOrEqualTo(100).WithMessage("Page size is too large");
            RuleFor(p => p.PoNumbers).Must(n => n.Length <= 100).When(p => p.PoNumbers is not null).WithMessage("Too many orders");
        }
    }

}
