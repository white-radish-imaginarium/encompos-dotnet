using EncomposApi.Models;

namespace EncomposApi
{
    public class ImageSellResult
    {
        public ImageSellCategoryModel[] Categories { get; set; }

        public ImageSellModel[] Products { get; set; }
    }
}
