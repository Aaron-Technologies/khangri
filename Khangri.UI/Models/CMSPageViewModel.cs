using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class CMSPageViewModel
    {
        public int CMSPageId { get; set; }

        [Required(ErrorMessage ="Please Enter Title")]
        public string Title { get; set; }

        public bool IsActive { get; set; }
    }
}
