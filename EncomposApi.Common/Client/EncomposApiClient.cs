using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace EncomposApi.Client
{
    public class EncomposApiClient
    {
        private readonly HttpClient _httpClient;

        public EncomposApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("encompos");
        }

        public async Task<JObject> GetOrCreateCustomerAsync(
            string email, string firstName, string lastName, string phone, bool? canText)
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
            using var response = await _httpClient.PutAsJsonAsync(requestUri, body);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsJObjectAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }

        public async Task<JObject> GetCustomerAsync(string email)
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
            var results = await QueryCustomersAsync(query);

            foreach (var result in results)
            {
                var jobj = (JObject)result;
                if (jobj["customer"] != null) return jobj;
            }

            return new JObject();
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

        public async Task<JArray> QueryCustomersAsync(CustomerQuery query)
        {
            var requestUri = $"/api/customers/query";
            using var response = await _httpClient.PostAsJsonAsync(requestUri, query);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsJArrayAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }

        public async Task<JArray> QueryInventoryAsync(InventoryQuery query)
        {
            JArray results = new();
            int pos = 0, len = query.Codes.Length;
            int batchSize = 100;
            while (pos < len)
            {
                string[] codes = new string[Math.Min(batchSize, len - pos)];
                Array.Copy(query.Codes, pos, codes, 0, codes.Length);
                JArray batch = await QueryInventoryOnceAsync(query with { Codes = codes });
                foreach (JToken token in batch)
                {
                    results.Add(token);
                }
                pos += batchSize;
            }

            return results;
        }

        private async Task<JArray> QueryInventoryOnceAsync(InventoryQuery query)
        {
            var requestUri = $"/api/inventory/query";
            using var response = await _httpClient.PostAsJsonAsync(requestUri, query);
            // var response = await _httpClient.PostAsJsonAsync(requestUri, query);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsJArrayAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }

        public async Task<JObject> PutInventoryAsync(string productCode, JObject model)
        {
            var requestUri = $"/api/inventory/{Uri.EscapeDataString(productCode)}";
            using var response = await _httpClient.PutAsJsonAsync(requestUri, model);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                return await response.Content.ReadAsJObjectAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }

        public async Task<JArray> QueryPurchaseOrdersAsync(PurchaseOrderQuery query)
        {
            var requestUri = $"/api/po/query";
            using var response = await _httpClient.PostAsJsonAsync(requestUri, query);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsJArrayAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }

        public async Task<JObject> PutPurchaseOrderLinesAsync(decimal poNumber, JArray lines)
        {
            var requestUri = $"/api/po/{poNumber}/lines";
            using var response = await _httpClient.PutAsJsonAsync(requestUri, lines);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsJObjectAsync();
            }
            throw await EncomposApiClientException.CreateAsync(response);
        }
    }
}
