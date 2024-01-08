using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Magazin_Online.Controllers
{
    public class BasketController : Controller
    {
        

            private readonly ApplicationDbContext db;

            private readonly UserManager<ApplicationUser> _userManager;

            private readonly RoleManager<IdentityRole> _roleManager;

            public BasketController(
                ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager
                )
            {
                db = context;

                _userManager = userManager;

                _roleManager = roleManager;
            }
        
            public IActionResult Index()
        {
            return View();
        }

        /*public void New()
        {
            Basket basket = new Basket();
            db.Baskets.Add(basket);
            db.SaveChanges();

        }*/
    }
}
