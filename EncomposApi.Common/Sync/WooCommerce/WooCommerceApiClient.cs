using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EncomposApi.Models;
using EncomposApi.Client;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using EncomposApi.Types;

namespace EncomposApi.Sync.WooCommerce;

public class WooCommerceApiClient
{
    private readonly static Lazy<JsonSerializerSettings> _serializerSettings =
        new(() => WooCommerceJsonUtilities.CreateSerializerSettings());

    private readonly static Lazy<JsonSerializer> _serializer =
        new(() => WooCommerceJsonUtilities.CreateSerializer());

    private readonly HttpClient _httpClient;

    public WooCommerceApiClient(IHttpClientFactory httpClientFactory, IOptionsSnapshot<WooCommerceOptions> options)
    {
        // Convert the username and password to Base64
        string baseAddress = options.Value.BaseAddress;
        string username = options.Value.Key;
        string password = options.Value.Secret;
        string creds = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(baseAddress);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", creds);
    }

    public JsonSerializerSettings SerializerSettings => _serializerSettings.Value;

    public JsonSerializer Serializer => _serializer.Value;

    public T Deserialize<T>(JToken token)
    {
        return token.ToObject<T>(Serializer);
    }

    public JToken Serialize<T>(T value)
    {
        return JToken.FromObject(value, Serializer);
    }

    public async Task<WooProductModel[]> ListProductsAsync(string sku = null)
    {
        var queryString = new QueryString();
        if (!string.IsNullOrEmpty(sku))
        {
            queryString = queryString.Add(nameof(sku), sku);
        }

        var requestUri = $"products{queryString}";

        using var response = await _httpClient.GetAsync(requestUri);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync();
            return json.Select(Deserialize<WooProductModel>).ToArray();
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<WooProductModel> UpdateProductAsync(WooProductModel product)
    {
        var requestUri = $"products/{product.Id}";
        var payload = product.ToUpdateModel();
        using var content = Serializer.CreateHttpContent(payload);
        using var response = await _httpClient.PutAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return Deserialize<WooProductModel>(json);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<WooProductModel> CreateProductAsync(WooProductModel product)
    {
        var requestUri = $"products";
        var payload = product.ToCreateModel();
        using var content = Serializer.CreateHttpContent(payload);
        using var response = await _httpClient.PostAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return Deserialize<WooProductModel>(json);
        }
        throw await ApiClientException.CreateAsync(response);
    }
}
