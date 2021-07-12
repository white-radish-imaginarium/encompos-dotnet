using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public static class ModelExtensions
    {
        public static PurchaseOrderLineModel CopyFrom(this PurchaseOrderLineModel line, CatalogItemModel item)
        {
            item.ItemNumber.WhenPresent(value => line.ItemNumber = value);
            item.ProductCode.WhenPresent(value => line.ProductCode = value);
            item.BrandName.WhenPresent(value => line.BrandName = value);
            item.Attribute.WhenPresent(value => line.Attribute = value);
            item.Description.WhenPresent(value => line.Description = value);
            item.ItemSize.WhenPresent(value => line.ItemSize = value);
            item.PackSize.WhenPresent(value => line.PackSize = (long)value);
            item.CaseCost.WhenPresent(value => line.CaseCost = value);
            item.MinOrderQty.WhenPresent(value => line.OrderQty = value);

            return line;
        }

        public static InventoryModel CopyFrom(this InventoryModel inv, CatalogItemModel item)
        {
            item.ProductCode.WhenPresent(value => inv.ProductCode = value);
            item.SupplierId.WhenPresent(value => inv.SupplierId = value);
            item.ItemNumber.WhenPresent(value => inv.ItemNumber = value);
            item.Description.WhenPresent(value => inv.Description = value);
            item.BrandName.WhenPresent(value => inv.BrandName = value);
            item.SRP.WhenPresent(value => inv.RetailPrice = value);
            item.UnitCost.WhenPresent(value => inv.UnitCost = value);
            item.ItemSize.WhenPresent(value => inv.ItemSize = value);
            item.MinOrderQty.WhenPresent(value => inv.MinOrderQty = value);
            item.PackSize.WhenPresent(value => inv.PackSize = value);
            item.CaseCost.WhenPresent(value => inv.CaseCost = value);
            item.DepartmentId.WhenPresent(value => inv.DepartmentId = value);

            return inv;
        }
    }
}
