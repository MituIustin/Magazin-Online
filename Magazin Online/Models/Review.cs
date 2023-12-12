namespace Magazin_Online.Models
{
    public class Review
    { 
        public int ReviewId { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser UserId { get; set; }
    }
}
