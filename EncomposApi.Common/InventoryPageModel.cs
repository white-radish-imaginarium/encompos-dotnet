using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi;

public record InventoryPageModel
{
    public string AfterProductCode { get; init; }

    public bool IncludeDiscontinued { get; init; }

    public decimal[] DepartmentIds { get; init; }

    public int PageSize { get; init; } = 100;
}

public class InventoryPageModelValidator : AbstractValidator<InventoryPageModel>
{
    public InventoryPageModelValidator()
    {
        RuleFor(p => p.PageSize).InclusiveBetween(1, 100);
    }
}
