using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EncomposApi.Enums
{
    public enum InventoryPresetType
    {
        None = 1,
        SingleWithDeposit = 2,
        FourPackWithDeposit = 3,
        SixPackWithDeposit = 4
    }
}
