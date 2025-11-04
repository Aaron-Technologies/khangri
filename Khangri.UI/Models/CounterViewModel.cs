using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class CounterViewModel
    {
        public int CounterId { get; set; }

        [Required(ErrorMessage ="Please Enter A Title")]
        public string Title { get; set; }
        public long  Value { get; set; }

        [Required(ErrorMessage ="Please Enter A Description")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Please Enter Icon Code")]
        public string Icon { get; set; }

        [Required(ErrorMessage ="Please Enter Link")]
        public string Link { get; set; }

        public bool IsActive { get; set; }
    }
}
