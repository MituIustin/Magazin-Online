namespace Magazin_Online.Models
{
    public class Article
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
