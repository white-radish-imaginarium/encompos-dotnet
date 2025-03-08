using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EncomposApi.Models;

namespace EncomposApi;

public class CustomerResult
{
    public string Id { get; set; }
    public string Email { get; set; }
    public CustomerModel Customer { get; set; }
    public CustomerModel[] OtherMatches { get; set; }

    [JsonIgnore]
    public int Status { get; set; } = 200;

    public static implicit operator JsonResult(CustomerResult result)
    {
        return new JsonResult(result)
        {
            StatusCode = result.Status
        };
    }
}
