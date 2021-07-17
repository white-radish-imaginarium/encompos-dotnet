using Newtonsoft.Json;

namespace EncomposApi.Common.Tests
{
    public class TestBase
    {
        static TestBase()
        {
            JsonConvert.DefaultSettings = JsonUtilities.CreateSettings;
        }
    }
}
