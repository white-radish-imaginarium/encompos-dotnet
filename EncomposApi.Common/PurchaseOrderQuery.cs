namespace EncomposApi
{
    public class PurchaseOrderQuery
    {
        public int? PageSize { get; set; }
        public decimal? BeforePoNumber { get; set; }

        public decimal[] PoNumbers { get; set; }

        public bool IncludeLines { get; set; }
    }
}
