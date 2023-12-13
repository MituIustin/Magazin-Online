using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }
        public string Location {  get; set; }
        public int ShippingPrice { get; set; }

        public virtual Basket Basket { get; set; }

    }
}
