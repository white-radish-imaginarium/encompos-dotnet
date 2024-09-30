using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace EncomposApi.Sync.WooCommerce
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WooProductType
    {
        [EnumMember(Value = "simple")]
        Simple = 0,

        [EnumMember(Value = "grouped")]
        Grouped = 1,

        [EnumMember(Value = "external")]
        External = 2,

        [EnumMember(Value = "variable")]
        Variable = 3,
    }
}
