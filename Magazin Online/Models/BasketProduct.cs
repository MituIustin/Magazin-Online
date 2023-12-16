﻿namespace Magazin_Online.Models
{
    public class BasketProduct
    {
        public int ? ProductId { get; set; }

        public int ? BasketId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Basket Basket { get; set; }

    }
}
