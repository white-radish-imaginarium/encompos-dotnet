using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using EncomposApi.Types.Optional;

namespace EncomposApi
{
    public static class JsonUtilities
    {
        public static JsonSerializerSettings CreateSettings() =>
            new()
            {
                ContractResolver = new OptionalContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                Converters = new List<JsonConverter> { new OptionalConverter() },
                Formatting = Formatting.Indented,
            };

        public static JsonSerializer CreateSerializer() =>
            JsonSerializer.Create(CreateSettings());
    }
}
