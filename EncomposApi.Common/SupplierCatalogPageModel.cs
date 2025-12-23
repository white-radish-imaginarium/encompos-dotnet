using FluentValidation;

namespace EncomposApi;

public record SupplierCatalogPageModel
{
    public decimal? AfterId { get; init; }

    public int PageSize { get; init; } = 100;
}

public class SupplierCatalogPageModelValidator : AbstractValidator<SupplierCatalogPageModel>
{
    public SupplierCatalogPageModelValidator()
    {
        RuleFor(p => p.PageSize).InclusiveBetween(1, 100);
    }
}
