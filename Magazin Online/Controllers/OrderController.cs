using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Online.Controllers
{
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public OrderController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }
        public IActionResult New()
        {
            var id_user = _userManager.GetUserId(User);

            var id_basket = db.Baskets
                .Where(b => b.UserId == id_user)
                .Select(b => b.BasketId)
                .FirstOrDefault();

            var basketProducts = db.BasketProducts
                .Where(bp => bp.BasketId == id_basket)
                .ToList();

            Order newOrder = new Order();
            
            foreach (var basketProduct in basketProducts)
            {
                newOrder.ProductIds.Add(basketProduct.ProductId.GetValueOrDefault());
            }

            db.Orders.Add(newOrder);
            db.BasketProducts.RemoveRange(basketProducts);
            db.SaveChanges();

            return View();
        }





    }
}
