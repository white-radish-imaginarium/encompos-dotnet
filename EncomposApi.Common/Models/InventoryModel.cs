using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EncomposApi.Enums;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public class InventoryModel //: ILabelingModel, IStockCheckModel
    {
        //public string Type { get; set; } = "inventory";
        [Required]
        public string ProductCode { get; set; }
        public Optional<string> BrandName { get; set; }
        public Optional<string> Description { get; set; }
        public Optional<string> ItemSize { get; set; }
        public Optional<string> Attribute { get; set; }
        public Optional<InventoryType> InventoryType { get; set; }

        public Optional<decimal> RetailPrice { get; set; }
        public Optional<decimal> UnitCost { get; set; }
        public Optional<decimal> Markup { get; set; }
        public Optional<decimal> Margin { get; set; }
        public Optional<decimal?> DefautQty { get; set; }
        public Optional<bool> IsWeighed { get; set; }

        public Optional<decimal> QtyOnHand { get; set; }
        public Optional<decimal?> MinQty { get; set; }
        public Optional<decimal?> MaxQty { get; set; }

        /// <summary>
        /// Physcial Inventory Date
        /// </summary>
        public Optional<DateTimeOffset> PiDate { get; set; }


        public Optional<decimal> DepartmentId { get; set; }
        public Optional<string> DepartmentName { get; set; }
        public Optional<bool> IsEbtEligible { get; set; }
        public Optional<IList<bool>> Taxes { get; set; }


        public Optional<decimal?> SupplierId { get; set; }
        public Optional<string> SupplierName { get; set; }
        public Optional<string> ItemNumber { get; set; }
        public Optional<decimal?> CaseCost { get; set; }
        public Optional<decimal?> PackSize { get; set; }
        public Optional<decimal> MinOrderQty { get; set; }

        /// <summary>
        /// Aliases from the inventory table
        /// </summary>
        public Optional<IList<AliasModel>> Aliases { get; set; }

        /// <summary>
        /// Aliases from the alias_codes table
        /// </summary>
        public Optional<IList<AliasModel>> MoreAliases { get; set; }

    }
}
