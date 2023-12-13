using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        //public virtual Category CategoryId { get; set; }
        
        //public virtual Basket BasketId { get; set; }

    }
}
