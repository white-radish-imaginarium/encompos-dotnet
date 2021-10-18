using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EncomposApi.Models;

namespace EncomposApi
{
    public class InventoryQueryResult
    {
        public string Code { get; set; }
        public InventoryModel Inventory { get; set; }
        public CatalogItemModel[] Catalogs { get; set; }
        public PurchaseOrderLineModel[] OpenOrders { get; set; }
        public PromotionItemModel[] Promotions { get; set; }
        public SalesModel[] RecentSales { get; set; }

        [JsonIgnore]
        public int Status { get; set; } = 200;

        public static implicit operator JsonResult(InventoryQueryResult result)
        {
            return new JsonResult(result)
            {
                StatusCode = result.Status
            };
        }
    }
}
