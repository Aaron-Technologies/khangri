using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class TeamMemberViewModel
    {
        public int TeamMemberId { get; set; }

        [Required(ErrorMessage ="Please Enter Team Member Name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please Select Image")]
        public int ImageId { get; set; }

        [Required(ErrorMessage ="Please Enter Caption")]
        public string Caption { get; set; }

        [Required(ErrorMessage ="Please Enter Designation")]
        public string Designation { get; set; }
        public bool IsActive { get; set; }
    }
}
