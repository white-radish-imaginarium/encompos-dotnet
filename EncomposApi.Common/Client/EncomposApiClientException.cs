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

        public static async Task<EncomposApiClientException> CreateAsync(HttpResponseMessage response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.ServiceUnavailable => CreateFromMessage(response.StatusCode, response.ReasonPhrase),
                _ => await CreateFromContentAsync(response)
            };
        }

        private static async Task<EncomposApiClientException> CreateFromContentAsync(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
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

        private static EncomposApiClientException CreateFromJson(HttpStatusCode statusCode, JObject body)
        {
            string message = null;
            if (body != null)
            {
                message = body["reason"]?.ToString();
            }

            return CreateFromMessage(statusCode, message);
        }

        private static EncomposApiClientException CreateFromMessage(HttpStatusCode statusCode, string message = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = $"{(int)statusCode}";
            }
            return new EncomposApiClientException(statusCode, message);
        }

    }

}
