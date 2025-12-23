using FluentValidation;

namespace EncomposApi;

public record SupplierCatalogQuery
{
    public SupplierCatalogSearchModel Search { get; init; }

    public SupplierCatalogPageModel Paging { get; init; }

    public decimal? SupplierId { get; init; }

    public string ItemNumber { get; init; }

    public string ProductCode { get; init; }

    public bool RequireDept { get; init; }
}

public class SupplierCatalogQueryValidator : AbstractValidator<SupplierCatalogQuery>
{
    public SupplierCatalogQueryValidator()
    {
        RuleFor(p => p.Search).SetValidator(new SupplierCatalogSearchModelValidator());
        RuleFor(p => p.Paging).SetValidator(new SupplierCatalogPageModelValidator());

        RuleFor(p => p)
            .Must(p => p.Search == null || p.Paging == null)
            .WithMessage("Search and Paging are mutually exclusive.");
    }
}
