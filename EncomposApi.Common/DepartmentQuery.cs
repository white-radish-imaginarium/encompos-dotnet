using FluentValidation;

namespace EncomposApi
{
    public record DepartmentQuery
    {
        public decimal[] Ids { get; init; }
        
        /// <summary>
        /// Include inventory defaults.
        /// </summary>
        public bool IncludeDefaults { get; init; }

        public bool IncludeCounts { get; init; }
    }

    public class DepartmentQueryValidator : AbstractValidator<DepartmentQuery>
    {
        public DepartmentQueryValidator()
        {
        }
    }
}
