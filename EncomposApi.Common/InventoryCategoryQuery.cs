using FluentValidation;

namespace EncomposApi;

public record InventoryCategoryQuery
{
}

public class InventoryCategoryQueryValidator : AbstractValidator<InventoryCategoryQuery>
{
    public InventoryCategoryQueryValidator()
    {
    }
}
