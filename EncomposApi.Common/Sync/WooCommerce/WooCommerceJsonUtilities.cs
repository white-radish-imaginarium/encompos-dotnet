using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using EncomposApi.Types.Optional;

namespace EncomposApi.Sync.WooCommerce
{
    public static class WooCommerceJsonUtilities
    {
        public static JsonSerializerSettings CreateSerializerSettings() =>
            new()
            {
                ContractResolver = new OptionalContractResolver { 
                    NamingStrategy = new SnakeCaseNamingStrategy { ProcessDictionaryKeys = true } 
                },
                Converters = new List<JsonConverter> { new OptionalConverter() },
                Formatting = Formatting.Indented,
            };

        public static JsonSerializer CreateSerializer() =>
            JsonSerializer.Create(CreateSerializerSettings());
    }
}
