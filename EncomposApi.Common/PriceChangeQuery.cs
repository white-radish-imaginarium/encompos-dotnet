using FluentValidation;
using Newtonsoft.Json;
using System;

namespace EncomposApi
{
    public record PriceChangeQuery
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]

        public decimal? BeforeId { get; init; }

        public decimal? SinceId { get; init; }

        public int? Limit { get; init; }

        public string[] ProductCodes { get; init; }
    }

    public class PriceChangeQueryValidator : AbstractValidator<PriceChangeQuery>
    {
        public PriceChangeQueryValidator()
        {
            RuleFor(p => p.Limit).InclusiveBetween(1, 500);
        }
    }
}
