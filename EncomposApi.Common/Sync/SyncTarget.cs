using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace EncomposApi.Sync;

[JsonConverter(typeof(StringEnumConverter))]
public enum SyncTarget
{
    [EnumMember(Value = "none")]
    None = 0,

    [EnumMember(Value = "woocommerce")]
    WooCommerce = 1,
}
