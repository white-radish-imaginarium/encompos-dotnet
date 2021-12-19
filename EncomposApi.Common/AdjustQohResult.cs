using EncomposApi.Models;

namespace EncomposApi
{
    public record AdjustQohResult
    {
        public InventoryModel Inventory { get; init; }
        public QohMovementModel[] QohMovement { get; init; }
    }
}
