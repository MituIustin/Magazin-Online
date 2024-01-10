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
        public IActionResult New(string AdresaLivrare)
        {
            var id_user = _userManager.GetUserId(User);

            var id_basket = db.Baskets
                .Where(b => b.UserId == id_user)
                .Select(b => b.BasketId)
                .FirstOrDefault();

            var basketProducts = db.BasketProducts
                .Where(bp => bp.BasketId == id_basket)
                .Include(bp => bp.Product)
                .ToList();

            Order newOrder = new Order
            {
                ProductIds = "",
                Products = new List<Product>()  
            };

            foreach (var basketProduct in basketProducts)
            {
                newOrder.ProductIds += basketProduct.ProductId.GetValueOrDefault();
                newOrder.ProductIds += ",";
                newOrder.Products.Add(basketProduct.Product);
            }

            var totalOrderPrice = basketProducts.Sum(bp => bp.Product.Price);

            newOrder.ShippingPrice = totalOrderPrice;
            newOrder.Location = AdresaLivrare;

            db.Orders.Add(newOrder);
            db.BasketProducts.RemoveRange(basketProducts);
            db.SaveChanges();

            return View();
        }




    }
}
