using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
   public class TourTypeViewModel
    {
        public int TourTypeId { get; set; }

        [Required(ErrorMessage ="Please Select Tour Type")]
        public string TourType { get; set; }

        [Required(ErrorMessage ="Please Enter Tour Description")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}



 