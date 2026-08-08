using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EncomposApi.Json;
using EncomposApi.Models;
using EncomposApi.Types;

namespace EncomposApi.Client;

public class EncomposApiClient
{
    private readonly static Lazy<JsonSerializer> _serializer = 
        new(() => JsonUtilities.CreateSerializer());

    private static JsonSerializer Serializer => _serializer.Value;

    private readonly HttpClient _httpClient;

    public EncomposApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("encompos");
    }

    public T Deserialize<T>(JToken token)
    {
        return token.ToObject<T>(Serializer);
    }

    public async Task<TypedJsonResult<CustomerModel>> GetOrCreateCustomerAsync(
        string email, 
        string firstName, 
        string lastName, 
        string phone, 
        bool? canText)
    {
        email = email?.ToLowerInvariant().Trim();
        var normalizedEmail = NormalizeEmail(email);

        var body = new 
        {
            email,
            otherEmails = email != normalizedEmail ? new[] { normalizedEmail } : null, 
            firstName, 
            lastName, 
            phone, 
            canText 
        };

        var requestUri = $"/api/customers/get-or-create";
        using var content = Serializer.CreateHttpContent(body);
        using var response = await _httpClient.PutAsync(requestUri, content);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<CustomerModel>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<CustomerModel>> GetCustomerAsync(string email, CancellationToken cancellationToken = default)
    {
        // check to see if we have an account using the raw email address first,
        // if the raw email address doesn't match the normalized address.
        email = email?.ToLowerInvariant().Trim();
        var normalizedEmail = NormalizeEmail(email);
        var query = new CustomerQuery
        {
            Emails = email == normalizedEmail 
                ? new[] { email } 
                : new[] { email, normalizedEmail }
        };
        var results = await QueryCustomersAsync(query, cancellationToken);

        foreach (var result in results)
        {
            var jobj = (JObject)result;
            if (jobj["customer"] != null)
            {
                return new TypedJsonResult<CustomerModel>(jobj, HttpStatusCode.OK);
            }
        }

        return new TypedJsonResult<CustomerModel>(null, HttpStatusCode.NotFound);
    }

    public string NormalizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;

        // everything to lowercase, trimmed. sorry rando email providers!
        email = email.ToLowerInvariant().Trim();

        // normalize gmail addresses
        if (email.EndsWith("@gmail.com"))
        {
            var localPart = email.Split('@')[0];
            localPart = Regex.Replace(localPart, "\\.", "");
            localPart = Regex.Replace(localPart, "\\+.*", ""); 
            email = localPart + "@gmail.com";
        }

        return email;
    }

    public async Task<JArray> QueryCustomersAsync(CustomerQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/customers/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsJArrayAsync(cancellationToken);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<DepartmentModel[]>> QueryDepartmentsAsync(DepartmentQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/departments/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync(cancellationToken);
            return new TypedJsonResult<DepartmentModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<SupplierModel[]>> QuerySuppliersAsync(SupplierQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/suppliers/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync(cancellationToken);
            return new TypedJsonResult<SupplierModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<InventoryCategoryModel[]>> QueryInventoryCategoriesAsync(InventoryCategoryQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/inventory/categories";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync(cancellationToken);
            return new TypedJsonResult<InventoryCategoryModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<NextAvailableProductCodeResult>> GetNextAvailableProductCodeAsync(NextAvailableProductCodeQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/inventory/next-code";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync(cancellationToken);
            return new TypedJsonResult<NextAvailableProductCodeResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<InventoryQueryResult[]>> QueryInventoryAsync(InventoryQuery query, CancellationToken cancellationToken = default)
    {
        JArray results = new();
        if (query.Codes == null)
        {
            JArray batch = await QueryInventoryOnceAsync(query, cancellationToken);
            results.Merge(batch);
        }
        else
        {
            int pos = 0, len = query.Codes.Length;
            int batchSize = 100;
            while (pos < len)
            {
                string[] codes = new string[Math.Min(batchSize, len - pos)];
                Array.Copy(query.Codes, pos, codes, 0, codes.Length);
                JArray batch = await QueryInventoryOnceAsync(query with { Codes = codes }, cancellationToken);
                results.Merge(batch);
                pos += batchSize;
            }
        }

        return new TypedJsonResult<InventoryQueryResult[]>(results);
    }

    private async Task<JArray> QueryInventoryOnceAsync(InventoryQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/inventory/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsJArrayAsync(cancellationToken);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<InventoryPutResult>> PutInventoryAsync(string productCode, JObject model)
    {
        var requestUri = $"/api/inventory/{Uri.EscapeDataString(productCode)}";
        using var content = Serializer.CreateHttpContent(model);
        using var response = await _httpClient.PutAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<InventoryPutResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<AdjustQohResult>> AdjustQohAsync(string productCode, AdjustQohModel model)
    {
        var requestUri = $"/api/inventory/{Uri.EscapeDataString(productCode)}/adjust-qoh";
        using var content = Serializer.CreateHttpContent(model);
        using var response = await _httpClient.PostAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<AdjustQohResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<QohMovementModel[]>> QueryQohMovementAsync(string productCode, QohMovementQuery query)
    {
        var requestUri = $"/api/inventory/{Uri.EscapeDataString(productCode)}/query-qoh-movement";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync();
            return new TypedJsonResult<QohMovementModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<PriceChangeResult>> QueryPriceChangesAsync(PriceChangeQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/inventory/query-price-changes";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync(cancellationToken);
            return new TypedJsonResult<PriceChangeResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<PurchaseOrderResult[]>> QueryPurchaseOrdersAsync(PurchaseOrderQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/po/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync(cancellationToken);
            return new TypedJsonResult<PurchaseOrderResult[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<SalesLineModel[]>> QuerySalesAsync(SalesQuery query, CancellationToken cancellationToken = default)
    {
        var requestUri = $"/api/sales/query";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync(cancellationToken);
            return new TypedJsonResult<SalesLineModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }

    public async Task<TypedJsonResult<PurchaseOrderResult>> PutPurchaseOrderLinesAsync(decimal poNumber, JArray lines)
    {
        var requestUri = $"/api/po/{poNumber}/lines";
        using var content = Serializer.CreateHttpContent(lines);
        using var response = await _httpClient.PutAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<PurchaseOrderResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<PurchaseOrderLineModel>> AddToPurchaseOrderAsync(AddToPurchaseOrderModel model)
    {
        var requestUri = $"/api/po/add-to-order";
        using var content = Serializer.CreateHttpContent(model);
        using var response = await _httpClient.PostAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<PurchaseOrderLineModel>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<ApiResult>> DeletePurchaseOrderLineAsync(decimal poNumber, decimal poLineId)
    {
        var requestUri = $"/api/po/{poNumber}/lines/{poLineId}";
        using var response = await _httpClient.DeleteAsync(requestUri);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync();
            return new TypedJsonResult<ApiResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<InventoryModel[]>> EnsureFixedPricingAsync()
    {
        var requestUri = $"/api/inventory/ensure-fixed-pricing";
        using var content = Serializer.CreateHttpContent(new object());
        using var response = await _httpClient.PostAsync(requestUri, content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJArrayAsync();
            return new TypedJsonResult<InventoryModel[]>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response);
    }

    public async Task<TypedJsonResult<UserResult>> AuthenticateUserAsync(UserAuthenticationQuery query, CancellationToken cancellationToken)
    {
        var requestUri = $"/api/users/authenticate";
        using var content = Serializer.CreateHttpContent(query);
        using var response = await _httpClient.PostAsync(requestUri, content, cancellationToken);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            var json = await response.Content.ReadAsJObjectAsync(cancellationToken);
            return new TypedJsonResult<UserResult>(json, response.StatusCode);
        }
        throw await ApiClientException.CreateAsync(response, cancellationToken);
    }
}
