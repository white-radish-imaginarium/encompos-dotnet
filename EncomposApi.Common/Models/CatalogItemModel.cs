using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class CatalogItemModel
{
    public decimal Id { get; set; }
    public Optional<decimal> SupplierId { get; set; }
    public Optional<string> SupplierName { get; set; }
    public Optional<string> ItemNumber { get; set; }
    public Optional<string> ProductCode { get; set; }
    public Optional<string> BrandName { get; set; }
    public Optional<string> Description { get; set; }
    public Optional<string> ItemSize { get; set; }
    public Optional<string> Attribute { get; set; }
    public Optional<decimal> DepartmentId { get; set; }
    public Optional<decimal> PackSize { get; set; }
    public Optional<decimal> CaseCost { get; set; }
    public Optional<decimal> UnitCost { get; set; }
    public Optional<decimal> SRP { get; set; }
    public Optional<decimal> MinOrderQty { get; set; } = 1;
}
