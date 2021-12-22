
using Newtonsoft.Json;

namespace EncomposApi.Models
{
    public class ImageModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public char Type { get; set; }

        public long Size { get; set; }

        [JsonIgnore]
        public byte[] Bytes { get; set; }
    }
}
