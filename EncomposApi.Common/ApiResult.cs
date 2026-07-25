using Newtonsoft.Json;

namespace EncomposApi;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ApiResult
{
    public ApiResult() { }

    public ApiResult(int status, string reason = null)
    {
        Configure(status, reason);
    }

    [JsonProperty("status")]
    public int? Status { get; set; }

    [JsonProperty("ok")]
    public bool Okay { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    public void Configure(int status, string message = null)
    {
        Status = status;

        if (status >= 200 && status < 300)
        {
            Okay = true;
            Message = message;
            return;
        }

        // if we actual have an error, use ApiError to determin the Message.
        var error = new ApiError(status, message);
        Okay = false;
        Message = error.Reason;
    }

    public static ApiResult Ok(string message = null) => new (200, message);
    
    public static ApiResult Created(string message = null) => new (201, message);
    
    public static ApiResult Accepted(string message = null) => new (202, message);
}
