using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class BasketProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BasketProductId { get; set; }

        public int? ProductId { get; set; }
        public int? BasketId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Basket? Basket { get; set; }
    }

}
