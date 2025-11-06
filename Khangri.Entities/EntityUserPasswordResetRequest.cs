namespace Khangri.Entities
{
    public class EntityUserPasswordResetRequest
    {
        public long UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
