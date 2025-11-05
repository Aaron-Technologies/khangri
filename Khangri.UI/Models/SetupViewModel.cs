using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Khangri.UI.Models

{
    public class SetupViewModel
    {
        public int SetUpId { get; set; }

        [Required(ErrorMessage ="Please Enter Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Enter an Introduction")]
        public string CompanyIntroduction { get; set; }

        [Required(ErrorMessage ="Please Enter caption")]
        public string Caption { get; set; }

        public string LogoPrimary { get; set; }
        public string LogoSecondary { get; set;}

        [Required(ErrorMessage ="Please Enter Phone Number")]
        public string PhoneNoPrimary { get; set; }

        [Required(ErrorMessage ="Please Enter WatsApp Number")]
        public string WhatsAppPrimary { get; set;}

        [Required(ErrorMessage ="Please Enter Email Id")]
        public string EmailPrimary { get;}

        [Required(ErrorMessage ="Please Enter Google Review Url")]
        public string GoogleReviewUrl { get; set; }

        [Required(ErrorMessage ="Please Enter FaceBook Url")]
        public string FBUrl { get; set; }

        [Required(ErrorMessage ="Please Enter Instagram Url")]
        public string InstagramUrl { get; set; }

        [Required(ErrorMessage ="Please Enter Twiter Url")]
        public string TwiterUrl { get; set; }

        [Required(ErrorMessage ="Please Enter YouTube Url")]
        public string YouTubeUrl { get; set; }

        [Required(ErrorMessage ="Please Enter Linkedin Url")]
        public string LinkedinUrl { get; set; }
        public string Favicon { get; set; }
    }
}
