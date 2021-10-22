using FluentValidation;
using Newtonsoft.Json;
using System;

namespace EncomposApi
{
    public record PriceChangeQuery
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool BeforeNow { get; init; }

        public decimal? BeforeId { get; init; }

        public decimal? SinceId { get; init; }

        public DateTimeOffset? SinceDate { get; init; }

        public int? Limit { get; init; }
    }

    public class PriceChangeQueryValidator : AbstractValidator<PriceChangeQuery>
    {
        public PriceChangeQueryValidator()
        {
            RuleFor(p => p.BeforeId).NotNull().When(p => p.SinceDate is null && p.SinceId is null && !p.BeforeNow);
            RuleFor(p => p.SinceId).NotNull().When(p => p.SinceDate is null && p.BeforeId is null && !p.BeforeNow);
            RuleFor(p => p.SinceDate).NotNull().When(p => p.SinceId is null && p.BeforeId is null && !p.BeforeNow);
            RuleFor(p => p.Limit).InclusiveBetween(1, 500);
        }
    }
}
