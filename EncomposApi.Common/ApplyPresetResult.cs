using EncomposApi.Models;

namespace EncomposApi
{
    public record ApplyPresetResult
    {
        public InventoryModel Inventory { get; init; }
    }
}
