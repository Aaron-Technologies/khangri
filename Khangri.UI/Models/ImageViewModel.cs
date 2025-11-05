using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class ImageViewModel
    {
        public int ImageId { get; set; }
        [Required(ErrorMessage ="Please Select a Image")]
        public string ImageName { get; set; }
        public bool IsActive { get; set; }
    }
}
