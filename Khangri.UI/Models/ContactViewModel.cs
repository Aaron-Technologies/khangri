using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class ContactViewModel
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage ="Please Enter Header Text")]
        public  string Header { get; set; }

        [Required(ErrorMessage ="Please Enter Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email address is not valid")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        [RegularExpression("^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Email address is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Enter Your Address")]
        public string Address { get; set; }

        public bool IsActive { get; set; }
        //public SetupViewModel Setup { get; set; } = new SetupViewModel();
        //public List<ContactViewModel> Contact { get; set; }= new List<ContactViewModel>();
    }
}
