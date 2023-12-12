namespace Magazin_Online.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Message { get; set; }
        public virtual ApplicationUser UserId { get; set; }
        public virtual Product ProductId { get; set; }
    }
}
