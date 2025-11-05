using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please Enter User Id")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
