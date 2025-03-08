using EncomposApi.Types.Optional;
using System.ComponentModel.DataAnnotations;

namespace EncomposApi.Models;

public class ImageSellCategoryModel
{
    [Required]
    public int Id { get; set; }
    public Optional<string> Description { get; set; }
    public Optional<long?> ImageId { get; set; }
    public Optional<bool> ShowOnPos { get; set; }

}
