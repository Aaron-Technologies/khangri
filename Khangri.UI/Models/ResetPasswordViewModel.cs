using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class ResetPasswordViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        [Required(ErrorMessage = "Please Enter New Password")]
        [MaxLength(50, ErrorMessage = "Password cannot be more than 50 characters long")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please Confirm New Password")]
        [MaxLength(50, ErrorMessage = "Password cannot be more than 50 characters long")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirem Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
