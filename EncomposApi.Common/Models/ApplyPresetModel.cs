
using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi.Models
{
    public class ApplyPresetModel
    {
        public string ProductCode { get; set; }

        public InventoryPresetType PresetType { get; set; }
    }

    public class ApplyPresetModelModelValidator : AbstractValidator<ApplyPresetModel>
    {
        public ApplyPresetModelModelValidator()
        {
            RuleFor(item => item.ProductCode).NotEmpty();
            RuleFor(item => item.PresetType).IsInEnum();
        }
    }
}
