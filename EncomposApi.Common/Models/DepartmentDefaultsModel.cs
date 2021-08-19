using System.Collections.Generic;
using EncomposApi.Types.Optional;

namespace EncomposApi.Models
{
    public class DepartmentDefaultsModel
    {
        public Optional<decimal?> ProductGroupId { get; set; }
        public Optional<IList<bool>> Taxes { get; set; }
        public Optional<decimal> Margin { get; set; }
        public Optional<bool> Ebt { get; set; }
    }
}
