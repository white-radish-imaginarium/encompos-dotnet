using System;
using System.Linq;
using Newtonsoft.Json;

namespace EncomposApi;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ApiError
{
    public ApiError() { }

    public ApiError(int status, string reason = null)
    {
        Configure(status, reason);
    }

    public int Status { get; set; } = 400;
    
    public string Error { get; set; }
    
    public string Reason { get; set; }

    public string[] StackTrace { get; set; }

    public object Details { get; set; }

    public void Configure(int status, string reason = null)
    {
        Status = status;

        if (status >= 200 && status < 300)
        {
            Error = "ok";
            Reason = reason ?? "Everything looks fine here, actually....";
            StackTrace = null;
            return;
        }

        if (status == 400)
        {
            Error = "bad_request";
            Reason = reason ?? "Bad request.";
            StackTrace = null;
            return;
        }

        if (status == 401)
        {
            Error = "unauthorized";
            Reason = reason ?? "Authorization required.";
            StackTrace = null;
            return;
        }

        if (status == 403)
        {
            Error = "forbidden";
            Reason = reason ?? "Authorization denied.";
            StackTrace = null;
            return;
        }

        if (status == 404)
        {
            Error = "not_found";
            Reason = reason ?? "Not found.";
            StackTrace = null;
            return;
        }

        if (status == 405)
        {
            Error = "method_not_allowed";
            Reason = reason ?? "Method not allowed.";
            StackTrace = null;
            return;
        }

        Error = "other";
        Reason = reason ?? "status " + status;
    }

    public ApiError CopyFrom(Exception ex)
    {
        Error = ex.GetType().Name;
        Reason = ex.Message;

        // StackTrace is null on an exception that was constructed but never thrown,
        // which is legal for a caller building an error response directly.
        StackTrace = ex.StackTrace?
            .Split(new[] { "\r\n" }, StringSplitOptions.None)
            .SelectMany(i => i.Split('\n'))
            .Select(i => i.Trim())
            .ToArray();

        return this;
    }

    public static ApiError BadRequest(string reason = null) => new ApiError(400, reason);

    public static ApiError Unauthorized(string reason = null) => new ApiError(401, reason);
    
    public static ApiError Forbidden(string reason = null) => new ApiError(403, reason);
    
    public static ApiError NotFound(string reason = null) => new ApiError(404, reason);
    
    public static ApiError Internal(Exception ex) => new ApiError(500).CopyFrom(ex);
}
