using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EncomposApi.Enums
{
    public enum InventoryPresetType
    {
        None = 0,
        SingleWithDeposit = 1,
        FourPackWithDeposit = 2,
        SixPackWithDeposit = 3,
        FourPackNoDeposit = 4,
        SixPackNoDeposit = 5
    }
}
