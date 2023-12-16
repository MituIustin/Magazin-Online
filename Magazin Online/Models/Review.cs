using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public float Rating { get; set; }
        public string ? Description { get; set; }

        [ForeignKey("User")]
        public string ? UserId { get; set; }
        public virtual ApplicationUser ? User { get; set; }

        [ForeignKey("Product")]
        public int ? ProductId { get; set; }
        public virtual Product ? Product { get; set; }
    }
}
