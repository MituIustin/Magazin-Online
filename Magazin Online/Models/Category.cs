namespace Magazin_Online.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ApplicationUser UserId { get; set; }
    }
}
