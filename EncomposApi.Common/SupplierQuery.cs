using FluentValidation;

namespace EncomposApi
{
    public record SupplierQuery
    {
    }

    public class SupplierQueryValidator : AbstractValidator<SupplierQuery>
    {
        public SupplierQueryValidator()
        {
        }
    }
}
