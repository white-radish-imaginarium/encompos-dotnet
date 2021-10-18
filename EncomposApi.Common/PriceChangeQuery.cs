using FluentValidation;
using System;

namespace EncomposApi
{
    public record PriceChangeQuery
    {
        public decimal? SinceId { get; init; }
        public DateTimeOffset? SinceDate { get; init; }
        public int? Limit { get; init; }
    }

    public class PriceChangeQueryValidator : AbstractValidator<PriceChangeQuery>
    {
        public PriceChangeQueryValidator()
        {
            RuleFor(p => p.SinceId).NotNull().When(p => p.SinceDate is null);
            RuleFor(p => p.SinceDate).NotNull().When(p => p.SinceId is null);
            RuleFor(p => p.Limit).InclusiveBetween(1, 500);
        }
    }
}
