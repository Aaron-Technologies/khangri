namespace Khangri.Entities
{
    public class EntityUser
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
        public string Password { get; set; }
        public long RowNo { get; set; }
    }
}
