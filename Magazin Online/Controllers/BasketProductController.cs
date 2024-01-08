using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Magazin_Online.Controllers
{
    public class BasketProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BasketProductController(
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

        public IActionResult Add(int id_prod)
        {
            var id_user = _userManager.GetUserId(User);

            ApplicationUser currentUser = db.Users.Find(id_user);

            if (currentUser.BasketId > 0)
            {
                BasketProduct basketProduct = new BasketProduct
                {
                    ProductId = id_prod,
                    BasketId = currentUser.BasketId.Value
                };

                db.BasketProducts.Add(basketProduct);
                db.SaveChanges();
            }
            else
            {
                Basket newBasket = new Basket();
                newBasket.UserId = id_user;
                db.Baskets.Add(newBasket);
                db.SaveChanges();

                currentUser.BasketId = newBasket.BasketId;
                db.SaveChanges();

                BasketProduct basketProduct = new BasketProduct
                {
                    ProductId = id_prod,
                    BasketId = newBasket.BasketId,
                };

                db.BasketProducts.Add(basketProduct);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int id_prod)
        {
            var id_user = _userManager.GetUserId(User);

            var basketProduct = db.BasketProducts
                .FirstOrDefault(bp => bp.ProductId == id_prod && bp.Basket.UserId == id_user);

            db.BasketProducts.Remove(basketProduct);
            db.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

    }
}