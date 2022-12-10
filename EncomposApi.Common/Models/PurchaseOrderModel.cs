using EncomposApi.Enums;
using EncomposApi.Types.Optional;
using System;
using System.ComponentModel.DataAnnotations;

namespace EncomposApi.Models
{
    public class PurchaseOrderModel
    {
        [Required]
        public decimal PoNumber { get; set; }
        public Optional<decimal> SupplierId { get; set; }
        public Optional<string> SupplierName { get; set; }

        public Optional<PurchaseOrderState> State { get; set; }

        public Optional<DateTimeOffset> DateCreated { get; set; }
        public Optional<DateTimeOffset> DateSent { get; set; }
        public Optional<DateTimeOffset> DateReceived { get; set; }

        public Optional<decimal> OrderTotal { get; set; }
        public Optional<string> InvoiceNumber { get; set; }
        public Optional<DateTimeOffset> LastModified { get; set; }
        public Optional<string> Note { get; set; }
    }
}
