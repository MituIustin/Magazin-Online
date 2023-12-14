using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Basket
    {
        [Key]
        public string BasketId { get; set; }

        public virtual ICollection<BasketProduct> ? BasketProducts { get; set; }

        [ForeignKey("User")]
        public string ? UserId { get; set; }
        public virtual ApplicationUser ? User { get; set; }


        [ForeignKey("Order")]
        public string ? OrderId { get; set; }
        public virtual Order ? Order { get; set; }

    }
}
