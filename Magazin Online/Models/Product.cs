namespace Magazin_Online.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public virtual ApplicationUser UserId { get; set; }
        public virtual Category CategoryId { get; set; }
        public virtual Basket BasketId { get; set; }

    }
}
