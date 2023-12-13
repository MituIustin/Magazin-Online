using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Order Order { get; set; }
    }
}
