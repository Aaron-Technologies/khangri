namespace Khangri.UI.Models
{
    public class BookingEnquiryMailRequest
    {
        public  string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public  string Date { get; set; }
        public int NoChildren { get; set; }
        public int Adults { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
