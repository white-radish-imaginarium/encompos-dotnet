using EncomposApi.Models;

namespace EncomposApi;

public record ApplyDepositTypeResult
{
    public InventoryModel Inventory { get; init; }
}
