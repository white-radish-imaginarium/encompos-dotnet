
using FluentValidation;

namespace EncomposApi.Models
{
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
            RuleFor(item => item.SupplierId).ScalePrecision(0, 5, ignoreTrailingZeros: true);
            RuleFor(item => item.ItemNumber).MaximumLength(20);
            RuleFor(item => item.ItemNumber).NotEmpty().When(i => string.IsNullOrEmpty(i.ProductCode));
            RuleFor(item => item.ProductCode).MaximumLength(15);
            RuleFor(item => item.ProductCode).NotEmpty().When(i => string.IsNullOrEmpty(i.ItemNumber));
            RuleFor(item => item.CaseCost).ScalePrecision(4, 19);
            RuleFor(item => item.PackSize).InclusiveBetween(1, uint.MaxValue);
            RuleFor(item => item.OrderQty).ScalePrecision(3, 10);
        }
    }
}
