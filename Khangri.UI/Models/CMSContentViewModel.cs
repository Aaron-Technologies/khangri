using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class CMSContentViewModel
    {
        [Required(ErrorMessage ="Please Enter Title")]
        public int CMSPageId { get; set; }

        [Required(ErrorMessage ="Please Enter Content")]
        public string Content { get; set; }
    }
}
