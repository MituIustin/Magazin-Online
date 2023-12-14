using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magazin_Online.Models
{
    public class Comment
    {
        [Key]
        public string CommentId { get; set; }
        public string Content { get; set; }
        public DateTime data_ora { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        [ForeignKey("Product")]
        public string? ProductId { get; set; }
        public virtual Product ? Product { get; set; }
    }
}
