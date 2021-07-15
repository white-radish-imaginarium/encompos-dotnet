using System;
using System.IO;
using System.Net.Http;
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
                    c.DefaultRequestHeaders.Add("X-Api-Key", options.ApiKey);
                    c.DefaultRequestHeaders.Add("User-Agent", string.IsNullOrEmpty(options.UserAgent) ? nameof(EncomposApiClient) : options.UserAgent);
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
