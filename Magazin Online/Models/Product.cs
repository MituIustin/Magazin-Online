using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }

        [ForeignKey("Basket")]
        public string? BasketId { get; set; }
        public virtual Basket? Basket { get; set; }


        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }


        [ForeignKey("Category")]
        public string? CategoryId { get; set; }
        public virtual Category ? Category { get; set; }


        [ForeignKey("Request")]
        public string? RequestId { get; set; }
        public virtual Request ? Request { get; set; }


        public virtual ICollection<Comment> ? Comments { get; set; }
        public virtual ICollection<Review> ? Reviews { get; set; }

       
 
    }
}
