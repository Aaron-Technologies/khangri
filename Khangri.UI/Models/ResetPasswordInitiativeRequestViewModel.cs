using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models
{
    public class ResetPasswordInitiativeRequestViewModel
    {
        [Required(ErrorMessage = "Please enter email id/mobile")]
        [MaxLength(50, ErrorMessage = "Login name can't be more than 50 characters")]
        public string LoginName { get; set; }
    }
}
