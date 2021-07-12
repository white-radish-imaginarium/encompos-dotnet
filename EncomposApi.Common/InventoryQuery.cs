namespace EncomposApi
{
    public class InventoryQuery
    {
        public string[] Codes { get; set; }
        
        /// <summary>
        /// Include catalog items in the results.
        /// </summary>
        public bool IncludeCatalogs { get; set; }
        
        /// <summary>
        /// Ignore aliases when looking up inventory.
        /// </summary>
        public bool IgnoreAliases { get; set; }

        public bool IncludeOpenOrders { get; set; }

        public bool IncludePromotions { get; set; }
    }
}
