using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EncomposApi.Models;

namespace EncomposApi;

public class InventoryPutResult
{
    public string Code { get; set; }
    public InventoryModel Inventory { get; set; }

    [JsonIgnore]
    public int Status { get; set; } = 200;

    public static implicit operator JsonResult(InventoryPutResult result)
    {
        return new JsonResult(result)
        {
            StatusCode = result.Status
        };
    }
}
