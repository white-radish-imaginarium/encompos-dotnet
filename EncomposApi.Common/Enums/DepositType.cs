using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EncomposApi.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DepositType
    {
        [EnumMember(Value = "none")]
        None = 0,

        [EnumMember(Value = "5c")]
        FiveCents = 1,

        [EnumMember(Value = "15c")]
        FifteenCents = 2,

        [EnumMember(Value = "20c")]
        TwentyCents = 3,

        [EnumMember(Value = "30c")]
        ThirtyCents = 4,

        [EnumMember(Value = "40c")]
        FortyCents = 5,
    }
}
