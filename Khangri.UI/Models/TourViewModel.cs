using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class TourViewModel
    {
        public int TourId { get; set; }

        [Required(ErrorMessage ="Please select Tour Type")]
        public int TourTypeId { get; set; }

        [Required(ErrorMessage ="Please Enter Tour Name")]
        public string TourName { get; set; }

        [Required(ErrorMessage = "Please Enter Tour Inroduction")]
        public string TourInroduction { get; set; }
        [Required(ErrorMessage = "Please Enter Tour Highlights")]
        public string TourHighlight { get; set; }
        [Required(ErrorMessage = "Please Enter Tour Overview")]
        public string TourOverview { get; set; }

        [Required(ErrorMessage ="Please Enter  No Of Days")]
        public int NoOfDays { get; set; }

        [Required(ErrorMessage ="Please Enter Tour Price")]
        public string Price { get; set; }
        public bool IsActive { get; set; }

        public  int  M20_ImageId { get; set; }
        public string ImageName { get; set; }
        public string Caption { get; set; }
    }
}
