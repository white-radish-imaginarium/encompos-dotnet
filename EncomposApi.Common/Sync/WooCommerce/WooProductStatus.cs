using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace EncomposApi.Sync.WooCommerce;

[JsonConverter(typeof(StringEnumConverter))]
public enum WooProductStatus
{
    [EnumMember(Value = "draft")]
    Draft = 0,

    [EnumMember(Value = "pending")]
    Pending = 1,

    [EnumMember(Value = "private")]
    Private = 2,

    [EnumMember(Value = "publish")]
    Publish = 3,
}
