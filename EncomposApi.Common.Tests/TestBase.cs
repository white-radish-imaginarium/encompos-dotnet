using EncomposApi.Types.Optional;
using Newtonsoft.Json;

namespace EncomposApi.Tests
{
    public class TestBase
    {
        static TestBase()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = OptionalContractResolver.Instance
                };
                settings.Converters.Add(new OptionalConverter());
                return settings;
            };
        }
    }
}
