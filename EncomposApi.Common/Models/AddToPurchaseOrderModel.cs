
using FluentValidation;

namespace EncomposApi.Models;

public class AddToPurchaseOrderModel
{
    public decimal SupplierId { get; set; }
    public string ItemNumber { get; set; }
    public string ProductCode { get; set; }
    public decimal CaseCost { get; set; }
    public long PackSize { get; set; }
    public decimal OrderQty { get; set; }
}

public class AddToPurchaseOrderModelValidator : AbstractValidator<AddToPurchaseOrderModel>
{
    public AddToPurchaseOrderModelValidator()
    {
        RuleFor(item => item.SupplierId).PrecisionScale(5, 0, true);
        RuleFor(item => item.ItemNumber).MaximumLength(20);
        RuleFor(item => item.ItemNumber).NotEmpty().When(i => string.IsNullOrEmpty(i.ProductCode));
        RuleFor(item => item.ProductCode).MaximumLength(15);
        RuleFor(item => item.ProductCode).NotEmpty().When(i => string.IsNullOrEmpty(i.ItemNumber));
        RuleFor(item => item.CaseCost).PrecisionScale(19, 4, true);
        RuleFor(item => item.PackSize).InclusiveBetween(1, uint.MaxValue);
        RuleFor(item => item.OrderQty).PrecisionScale(10, 3, true);
    }
}
