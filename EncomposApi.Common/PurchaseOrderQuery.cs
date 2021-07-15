namespace EncomposApi
{
    public record PurchaseOrderQuery
    {
        public int? PageSize { get; init; }
        public decimal? BeforePoNumber { get; init; }

        public decimal[] PoNumbers { get; init; }

        public bool IncludeLines { get; init; }
    }
}
