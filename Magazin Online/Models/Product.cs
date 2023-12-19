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
        public int Price { get; set; }
        public bool IsAccepted { get; set; }
        public byte[]? Photo { get; set; }
        public float? rating { get; set; }

        [ForeignKey("Basket")]
        public int? BasketId { get; set; }
        public virtual Basket? Basket { get; set; }


        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }


        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public virtual Category ? Category { get; set; }


        [ForeignKey("Request")]
        public int? RequestId { get; set; }
        public virtual Request ? Request { get; set; }



        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }
        public virtual ICollection<Comment> ? Comments { get; set; }
        public virtual ICollection<Review> ? Reviews { get; set; }
        public virtual ICollection<BasketProduct> ? BasketProducts { get; set; }

    }
}
