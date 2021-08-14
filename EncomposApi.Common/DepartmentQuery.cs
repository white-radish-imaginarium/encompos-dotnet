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

        public DepartmentQuery Normalize()
        {
            if (Nested && IncludeParents) return this with { IncludeParents = false };
            return this;
        }
    }
}
