using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace EncomposApi.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PurchaseOrderState
    {
        [EnumMember(Value = "draft")]
        Draft = 0,

        [EnumMember(Value = "sent")]
        Sent = 1,

        [EnumMember(Value = "received")]
        Received = 2,
    }
}
