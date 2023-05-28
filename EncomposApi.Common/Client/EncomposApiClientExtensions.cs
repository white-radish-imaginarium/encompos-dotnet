using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EncomposApi.Client
{
    public static class EncomposApiClientExtensions
    {
        private static void SerializeToStream(this JsonSerializer serializer, object value, Stream stream)
        {
            using StreamWriter sw = new(stream, new UTF8Encoding(false), 1024, true);
            using JsonTextWriter jw = new(sw) { Formatting = Formatting.None };
            serializer.Serialize(jw, value);
            jw.Flush();
        }

        // based on: https://johnthiriet.com/efficient-post-calls/
        public static HttpContent CreateHttpContent(this JsonSerializer serializer, object content)
        {
            if (content == null) return default;

            MemoryStream ms = new();
            serializer.SerializeToStream(content, ms);
            ms.Seek(0, SeekOrigin.Begin);
            StreamContent streamContent = new(ms);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return streamContent;
        }

        public static async Task<JObject> ReadAsJObjectAsync(this HttpContent content, CancellationToken cancellationToken = default)
        {
            using var stream = await content.ReadAsStreamAsync(cancellationToken);
            using var streamReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(streamReader);
            return await JObject.LoadAsync(jsonReader, cancellationToken);
        }

        public static async Task<JArray> ReadAsJArrayAsync(this HttpContent content, CancellationToken cancellationToken = default)
        {
            using var stream = await content.ReadAsStreamAsync(cancellationToken);
            using var streamReader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(streamReader);
            return await JArray.LoadAsync(jsonReader, cancellationToken);
        }

        public static IServiceCollection AddEncomposApiClient(this IServiceCollection services)
        {
            services.AddHttpClient("encompos", (p, c) =>
                {
                    EncomposApiClientOptions options = p.GetService<IOptions<EncomposApiClientOptions>>().Value;
                    c.BaseAddress = new Uri(options.BaseUrl);
                    options.ConfigureHeaders(c.DefaultRequestHeaders);
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new SocketsHttpHandler
                    {
                        UseCookies = false,
                        MaxConnectionsPerServer = 4,
                        PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2), // the default
                        PooledConnectionLifetime = TimeSpan.FromMinutes(4)
                    };
                });

            services.AddSingleton<EncomposApiClient>();

            return services;
        }
    }
}
