using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class PurchaseOrderLineModel
{
    public Optional<decimal> Id { get; set; }


    public Optional<decimal> PoNumber { get; set; }
    public Optional<PurchaseOrderModel> Order { get; set; }

    public Optional<string> ProductCode { get; set; }
    public Optional<string> ItemNumber { get; set; }

    public Optional<decimal> OrderQty { get; set; }
    public Optional<decimal> CaseCost { get; set; }
    public Optional<long> PackSize { get; set; }

    public Optional<decimal> ReceiveQty { get; set; }
    public Optional<decimal> InvoiceQty { get; set; }

    // TODO: break out into ProductModel
    public Optional<string> BrandName { get; set; }
    public Optional<string> Description { get; set; }
    public Optional<string> ItemSize { get; set; }
    public Optional<string> Attribute { get; set; }
}
