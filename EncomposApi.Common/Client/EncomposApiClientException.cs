using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EncomposApi.Client
{
    public class EncomposApiClientException : Exception
    {
        public EncomposApiClientException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        public static EncomposApiClientException Create(HttpStatusCode statusCode, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = $"{(int)statusCode}";
            }
            return new EncomposApiClientException(statusCode, message);
        }

        public static EncomposApiClientException Create(HttpStatusCode statusCode, JObject body)
        {
            string message = null;
            if (body != null)
            {
                message = body["reason"]?.ToString();
            }

            return Create(statusCode, message);
        }

        public static async Task<EncomposApiClientException> CreateAsync(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content)) return Create(response.StatusCode);
            try
            {
                var json = JObject.Parse(content);
                return Create(response.StatusCode, json);
            }
            catch
            {
                return Create(response.StatusCode, content);
            }
        }
    }

}
