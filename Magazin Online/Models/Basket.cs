namespace Magazin_Online.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ApplicationUser UserId { get; set; }
        public virtual Order OrderId { get; set; }

    }
}
