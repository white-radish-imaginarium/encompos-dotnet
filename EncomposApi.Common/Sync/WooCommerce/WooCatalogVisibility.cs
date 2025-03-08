using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace EncomposApi.Sync.WooCommerce;

[JsonConverter(typeof(StringEnumConverter))]
public enum WooCatalogVisibility
{
    [EnumMember(Value = "visible")]
    Visible = 0,

    [EnumMember(Value = "catalog")]
    Catalog = 1,

    [EnumMember(Value = "search")]
    Search = 2,

    [EnumMember(Value = "hidden")]
    Hidden = 3,
}
