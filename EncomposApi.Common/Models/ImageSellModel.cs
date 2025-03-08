using EncomposApi.Types.Optional;
using System.ComponentModel.DataAnnotations;

namespace EncomposApi.Models;

public class ImageSellModel
{
    [Required]
    public string ProductCode { get; set; }

    public Optional<int?> PluCategoryId { get; set; }

    public Optional<string> Description { get; set; }

    public Optional<long?> ImageId { get; set; }
}
