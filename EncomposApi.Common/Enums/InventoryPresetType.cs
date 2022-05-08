using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EncomposApi.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InventoryPresetType
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "single-with-deposit")]
        SingleWithDeposit,

        [EnumMember(Value = "four-pack-with-deposit")]
        FourPackWithDeposit,

        [EnumMember(Value = "six-pack-with-deposit")]
        SixPackWithDeposit
    }
}
