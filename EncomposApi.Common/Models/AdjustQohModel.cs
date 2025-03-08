
using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi.Models;

public class AdjustQohModel
{
    public string ProductCode { get; set; }
    public decimal Qty { get; set; }
    public MovementType Type { get; set; } = MovementType.ManualAdjustment;
    public MovementReason Reason { get; set; } = MovementReason.PhysicalInvAdjustment;
}

public class AdjustInventoryModelValidator : AbstractValidator<AdjustQohModel>
{
    public AdjustInventoryModelValidator()
    {
        RuleFor(item => item.ProductCode).NotEmpty();
        RuleFor(item => item.Qty).NotEqual(0);
    }
}
