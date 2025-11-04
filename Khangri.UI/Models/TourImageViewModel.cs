using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class TourImageViewModel
    {
        public int TourImageId { get; set; }

        public int TourId { get; set; }

        [Required(ErrorMessage ="Please select your Image")]
        public int ImageId { get; set; }

        [Required(ErrorMessage ="Please select TourName")]
        public string TourName { get; set; }

        [Required(ErrorMessage ="Please Enter Caption")]
        public string Caption { get; set; }
        public bool IsActive { get; set; }
    }
}
