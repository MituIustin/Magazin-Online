using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
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
        public string PhoneNumber {  get; set; }

        public virtual Basket Basket { get; internal set; }

        public virtual ICollection<Request> Requests { get; set; }
        
    }
}
