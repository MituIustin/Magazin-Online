namespace Magazin_Online.Models
{
    public class BasketProduct
    {
        public string ? ProductId { get; set; }

        public string ? BasketId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Basket Basket { get; set; }

    }
}
