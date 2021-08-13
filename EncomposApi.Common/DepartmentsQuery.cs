namespace EncomposApi
{
    public record DepartmentsQuery
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

        public DepartmentsQuery Normalize()
        {
            if (Nested && IncludeParents) return this with { IncludeParents = false };
            return this;
        }
    }
}
