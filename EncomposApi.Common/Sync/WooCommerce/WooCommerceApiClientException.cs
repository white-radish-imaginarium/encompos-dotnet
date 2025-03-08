using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EncomposApi.Client;

public class WooCommerceApiClientException : Exception
{
    public WooCommerceApiClientException(HttpStatusCode statusCode, string message, object details = null) : base(message)
    {
        StatusCode = statusCode;
        Details = details;
    }

    public HttpStatusCode StatusCode { get; }

    public object Details { get; }

    public static async Task<WooCommerceApiClientException> CreateAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        return response.StatusCode switch
        {
            HttpStatusCode.ServiceUnavailable => CreateFromMessage(response.StatusCode, response.ReasonPhrase),
            _ => await CreateFromContentAsync(response, cancellationToken)
        };
    }

    private static async Task<WooCommerceApiClientException> CreateFromContentAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        string content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (string.IsNullOrEmpty(content)) return CreateFromMessage(response.StatusCode);
        try
        {
            var json = JObject.Parse(content);
            return CreateFromJson(response.StatusCode, json);
        }
        catch
        {
            return CreateFromMessage(response.StatusCode, content);
        }
    }

    private static WooCommerceApiClientException CreateFromJson(HttpStatusCode statusCode, JObject body)
    {
        string reason = null;
        object details = null;
        if (body != null)
        {
            reason = body["reason"]?.ToString();
            details = body["details"];
        }

        return CreateFromMessage(statusCode, reason, details);
    }

    private static WooCommerceApiClientException CreateFromMessage(HttpStatusCode statusCode, string message = null, object details = null)
    {
        if (string.IsNullOrEmpty(message))
        {
            message = $"{(int)statusCode}";
        }
        return new WooCommerceApiClientException(statusCode, message, details);
    }

}
