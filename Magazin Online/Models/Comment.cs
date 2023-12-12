using NuGet.ContentModel;

namespace Magazin_Online.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public virtual ApplicationUser UserId { get; set; }
    }
}
