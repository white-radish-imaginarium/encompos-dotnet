using System.Net.Http.Headers;

namespace EncomposApi.Client;

public class EncomposApiClientOptions
{
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
    public string UserAgent { get; set; }

    public void ConfigureHeaders(HttpRequestHeaders headers)
    {
        headers.Add("X-Api-Key", ApiKey);
        headers.Add("User-Agent", string.IsNullOrEmpty(UserAgent) ? nameof(EncomposApiClient) : UserAgent);
    }
}
