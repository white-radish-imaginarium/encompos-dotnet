using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using EncomposApi.Types.Optional;

namespace EncomposApi.Json
{
    public static class JsonUtilities
    {
        public static JsonSerializerSettings CreateSerializerSettings() =>
            new()
            {
                ContractResolver = new OptionalContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                Converters = new List<JsonConverter> { new OptionalConverter() },
                Formatting = Formatting.Indented,
            };

        public static JsonSerializer CreateSerializer() =>
            JsonSerializer.Create(CreateSerializerSettings());
    }
}
