using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class LocationImageViewModel
    {
        public int LocationImageId { get; set; }

        [Required(ErrorMessage ="Please Select An Image")]
        public int ImageId { get; set; }

        [Required(ErrorMessage ="Please Select A Location")]
        public long LocationId { get; set; }

        [Required(ErrorMessage ="Please Enter a Caption")]
        public string Caption { get; set; }
        public bool IsActive { get; set; }

    }
}
