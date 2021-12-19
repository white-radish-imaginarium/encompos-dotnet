using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi
{
    public record QohMovementQuery
    {
        public string ProductCode { get; init; }

        public long? Id { get; init; }

        public decimal? BeforeId { get; init; }

        public MovementReason[] Reasons { get; init; }
    }

    public class QohMovementQueryValidator : AbstractValidator<QohMovementQuery>
    {
        public QohMovementQueryValidator()
        {
            RuleFor(p => p.ProductCode).NotEmpty();
        }
    }
}
