using EncomposApi.Types.Optional;

namespace EncomposApi.Models;

public class DepartmentModel
{
    public decimal Id { get; set; }
    public Optional<string> Name { get; set; }
    public Optional<decimal?> ParentId { get; set; }
    public Optional<DepartmentDefaultsModel> Defaults { get; set; }
    public Optional<int> InventoryCount { get; set; }
    public Optional<int> InactiveCount { get; set; }
}
