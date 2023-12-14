using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Magazin_Online.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public string Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string ? PhoneNumber {  get; set; }


        [ForeignKey("Basket")]
        public string? BasketId { get; set; }
        public virtual Basket ? Basket { get; set; }


        public virtual ICollection<Request> ? Requests { get; set; }
        public virtual ICollection<Comment> ? Comments { get; set; }
        public virtual ICollection<Review> ? Reviews { get; set; }
        public virtual ICollection<Product> ? Products { get; set; }
        public virtual ICollection<Category> ? Categories { get; set; }


    }
}
