namespace Khangri.UI.Models
{
    public class UserViewModel
    {
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserType { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DateTime EntryDateTime { get; set; }
        public bool IsActive { get; set; }
        public long RowNo { get; set; }
        public string UserFullName 
        { 
            get
            {
                return $"{FName} {LName}";
            }
        }
    }
}
