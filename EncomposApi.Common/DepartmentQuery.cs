using FluentValidation;

namespace EncomposApi
{
    public record DepartmentQuery
    {
        /// <summary>
        /// Nests subdepartments.
        /// </summary>
        public bool Nested { get; init; }

        /// <summary>
        /// Include inventory defaults.
        /// </summary>
        public bool IncludeDefaults { get; init; }

        /// <summary>
        /// Include parent departments. Only honored when Nested = false.
        /// </summary>
        public bool IncludeParents { get; init; }
    }

    public class DepartmentQueryValidator : AbstractValidator<DepartmentQuery>
    {
        public DepartmentQueryValidator()
        {
            RuleFor(p => p.IncludeParents).Must(i => !i).When(p => p.Nested).WithMessage("Cannot include parents in a nested result.");
        }
    }
}
