using EncomposApi.Enums;
using FluentValidation;

namespace EncomposApi.Models
{
    public class ApplyDepositTypeModel
    {
        public string ProductCode { get; set; }

        public DepositType DepositType { get; set; }
    }

    public class ApplyDepositTypeModelValidator : AbstractValidator<ApplyDepositTypeModel>
    {
        public ApplyDepositTypeModelValidator()
        {
            RuleFor(item => item.ProductCode).NotEmpty();
            RuleFor(item => item.DepositType).IsInEnum();
        }
    }
}
