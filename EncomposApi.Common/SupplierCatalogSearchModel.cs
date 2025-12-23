using FluentValidation;

namespace EncomposApi;

public record SupplierCatalogSearchModel
{
    public string SearchTerm { get; init; }

    public int PageIndex { get; init; } = 0;

    public int PageSize { get; init; } = 100;
}

public class SupplierCatalogSearchModelValidator : AbstractValidator<SupplierCatalogSearchModel>
{
    public SupplierCatalogSearchModelValidator()
    {
        RuleFor(p => p.SearchTerm).NotEmpty();
        RuleFor(p => p.PageIndex).GreaterThanOrEqualTo(0);
        RuleFor(p => p.PageSize).InclusiveBetween(1, 100);
    }
}
