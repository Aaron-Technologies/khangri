using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class SliderImageViewModel
    {
        public int SliderImageId { get; set; }
        public int PageTypeId { get; set; } = 1;
        public string ImageName { get; set; }
        public string Caption { get; set; }
        public string Details1 { get; set; }
        public string Details2 { get; set; }
        public string Link1 { get; set; }
        public string Link2 { get; set; }
        public bool IsActive { get; set; }
    }
}
