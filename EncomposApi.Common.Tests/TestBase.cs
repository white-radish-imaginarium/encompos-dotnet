using Newtonsoft.Json;
using EncomposApi.Json;

namespace EncomposApi.Common.Tests;

public class TestBase
{
    static TestBase()
    {
        JsonConvert.DefaultSettings = JsonUtilities.CreateSerializerSettings;
    }
}
