using Microsoft.AspNetCore.Mvc.Rendering;

namespace Khangri.UI.Helpers
{
    public class ImageSelectListItem : SelectListItem
    {
        public ImageSelectListItem(string text, string value) : base(text, value)
        {
           
        }

        public string ImageUrl { get; set; }
        public int Id { get; set; }
    }
}
