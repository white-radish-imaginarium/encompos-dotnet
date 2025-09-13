using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi;

public record InventorySearchModel
{
    public string SearchTerm { get; init; }

    public SearchField SearchFields { get; init; } = SearchField.Brand | SearchField.Description;

    public int PageIndex { get; init; } = 0;

    public int PageSize { get; init; } = 100;

    public bool IncludeDiscontinued { get; init; } = true;
}

public class InventorySearchModelValidator : AbstractValidator<InventorySearchModel>
{
    public InventorySearchModelValidator()
    {
        RuleFor(p => p.SearchTerm).NotEmpty();
        RuleFor(p => p.PageIndex).GreaterThanOrEqualTo(0);
        RuleFor(p => p.PageSize).InclusiveBetween(1, 100);
    }
}
