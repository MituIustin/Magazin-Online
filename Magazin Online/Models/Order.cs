using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string ? Location {  get; set; }
        public int? ShippingPrice { get; set; }

        [ForeignKey("Basket")]
        public int? BasketId { get; set; }
        public virtual Basket ? Basket { get; set; }


        [NotMapped]
        private ICollection<int>? _productIds { get; set; } = new List<int>();

        [Column("ProductIds")]
        public string? ProductIds { get; set;}

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();


    }
}
