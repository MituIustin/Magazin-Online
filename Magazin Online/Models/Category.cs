using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser ? User { get; set; }


        public virtual ICollection<Product> ? Products { get; set; }
    }
}
