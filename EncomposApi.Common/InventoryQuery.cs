namespace EncomposApi
{
    public record InventoryQuery
    {
        public string[] Codes { get; init; }
        
        /// <summary>
        /// Include catalog items in the results.
        /// </summary>
        public bool IncludeCatalogs { get; init; }
        
        /// <summary>
        /// Ignore aliases when looking up inventory.
        /// </summary>
        public bool IgnoreAliases { get; init; }

        public bool IncludeOpenOrders { get; init; }

        public bool IncludePromotions { get; init; }
    }
}
