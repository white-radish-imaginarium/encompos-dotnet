using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace EncomposApi.Enums
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchField
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "brand")]
        Brand = 1,

        [EnumMember(Value = "description")]
        Description = 2,

        [EnumMember(Value = "both")]
        Both = Brand | Description,
    }
}
