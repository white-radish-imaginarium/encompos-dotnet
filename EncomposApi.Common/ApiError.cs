using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace EncomposApi;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ApiError : IActionResult
{
    public ApiError() { }
    
    public ApiError(int status, string reason = null)
    {
        Configure(status, reason);
    }

    public ApiError(ModelStateDictionary modelState)
    {
        var result = new BadRequestObjectResult(modelState);
        Configure(result.StatusCode ?? 400);
        Details = result.Value;
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
        StackTrace = ex.StackTrace
            .Split(new[] { "\r\n" }, StringSplitOptions.None)
            .SelectMany(i => i.Split('\n'))
            .Select(i => i.Trim())
            .ToArray();

        return this;
    }

    public static ApiError BadRequest(string reason = null) => new ApiError(400, reason);
    
    public static ApiError Forbidden(string reason = null) => new ApiError(403, reason);
    
    public static ApiError NotFound(string reason = null) => new ApiError(404, reason);
    
    public static ApiError Internal(Exception ex) => new ApiError(500).CopyFrom(ex);

    public static implicit operator JsonResult(ApiError err)
    {
        return new JsonResult(err)
        {
            StatusCode = err.Status
        };
    }

    public Task ExecuteResultAsync(ActionContext context)
    {
        var jsonResult = (JsonResult)this;
        return jsonResult.ExecuteResultAsync(context);
    }
}
