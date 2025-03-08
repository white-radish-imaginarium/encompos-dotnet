using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EncomposApi.Models;

namespace EncomposApi;

public class PurchaseOrderResult
{
    public decimal PoNumber { get; set; }
    public PurchaseOrderModel Order { get; set; }
    public PurchaseOrderLineModel[] Lines { get; set; }

    [JsonIgnore]
    public int Status { get; set; } = 200;

    public static implicit operator JsonResult(PurchaseOrderResult result)
    {
        return new JsonResult(result)
        {
            StatusCode = result.Status
        };
    }
}
