using EncomposApi.Types.Optional;
using Newtonsoft.Json;

namespace EncomposApi.Models
{
    public class UserModel
    {
        public Optional<string> Id { get; set; }
        public Optional<string> FirstName { get; set; }
        public Optional<string> LastName { get; set; }
        public Optional<string> TagName { get; set; }

        public Optional<bool> Register { get; set; } = false;
        public Optional<bool> Backoffice { get; set; } = false;

        public Optional<int?> Rank { get; set; }
        public Optional<long?> PermissionGroup { get; set; }
        public Optional<string> PermissionGroupName { get; set; }
        public Optional<int?> Status { get; set; }

        [JsonIgnore]
        public Optional<string> PIN { get; set; }
    }
}
