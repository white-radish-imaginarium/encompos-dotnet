using System;
using FluentValidation;

namespace EncomposApi;

public record SalesQuery
{
    public string[] ProductCodes { get; init; }

    public DateTime FromDate { get; init; }

    public DateTime ToDate { get; init; }
}

public class SalesQueryValidator : AbstractValidator<SalesQuery>
{
    public SalesQueryValidator()
    {
        RuleFor(query => query.ProductCodes)
            .NotEmpty()
            .Must(productCodes => productCodes.Length <= 100)
            .When(query => query.ProductCodes is not null)
            .WithMessage("Too many product codes");
        RuleForEach(query => query.ProductCodes).NotEmpty();
        RuleFor(query => query.FromDate).NotEmpty();
        RuleFor(query => query.ToDate)
            .NotEmpty()
            .GreaterThanOrEqualTo(query => query.FromDate);
    }
}
