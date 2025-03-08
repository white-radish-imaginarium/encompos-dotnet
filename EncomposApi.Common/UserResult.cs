using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EncomposApi.Models;

namespace EncomposApi;

public class UserResult
{
    public UserModel User { get; set; }

    [JsonIgnore]
    public int Status { get; set; } = 200;

    public static implicit operator JsonResult(UserResult result)
    {
        return new JsonResult(result)
        {
            StatusCode = result.Status
        };
    }
}
