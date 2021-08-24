using System.Linq;
using FluentValidation;

namespace EncomposApi
{
    public record NextAvailableProductCodeQuery
    {
        /// <summary>
        /// Nests subdepartments.
        /// </summary>
        public string Prefix { get; init; }

        /// <summary>
        /// Include inventory defaults.
        /// </summary>
        public int Length { get; init; }

        public int? MinValue { get; init; }
    }

    public class NextAvailableProductCodeQueryValidator : AbstractValidator<NextAvailableProductCodeQuery>
    {
        public NextAvailableProductCodeQueryValidator()
        {
            RuleFor(p => p.Prefix)
                .Must(v => v.All(char.IsLetterOrDigit))
                .When(v => !string.IsNullOrEmpty(v.Prefix))
                .WithMessage("Prefix must be alphanumeric");

            RuleFor(p => p.Length).LessThanOrEqualTo(15);
            RuleFor(p => p.Length)
                .Must((model, length) => length > model.Prefix.Length)
                .When(v => v.Prefix != null)
                .WithMessage("Length must be longer than the prefix.");

            RuleFor(p => p.Length)
                .Must((model, length) => length - model.Prefix.Length < 5)
                .When(v => v.Prefix != null)
                .WithMessage("Suffix length must be less than 5.");

            RuleFor(p => p.MinValue).GreaterThanOrEqualTo(0);
        }
    }
}
