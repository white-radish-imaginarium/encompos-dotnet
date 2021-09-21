using System;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public class SalesModel
    {
        public Optional<string> ProductCode { get; set; }
        public Optional<DateTimeOffset> DateSold { get; set; }
        public decimal Qty { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalRetail { get; set; }
    }
}
