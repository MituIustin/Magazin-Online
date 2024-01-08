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

            // Creați un nou obiect Order
            Order newOrder = new Order
            {
                // Alte proprietăți ale obiectului Order pot fi completate aici, în funcție de nevoi
            };

            // Adăugați fiecare produs din coș la lista de ID-uri de produse a comenzii
            foreach (var basketProduct in basketProducts)
            {
                newOrder.ProductIds.Add(basketProduct.ProductId.GetValueOrDefault());
            }

            // Adăugați comanda în contextul bazei de date
            db.Orders.Add(newOrder);

            // Ștergeți produsele din coș (opțional, în funcție de cerințe)
            db.BasketProducts.RemoveRange(basketProducts);

            // Salvați modificările în baza de date
            db.SaveChanges();

            // Puteți redirecționa utilizatorul către o altă pagină sau puteți returna o vedere corespunzătoare
            return View();
        }





    }
}
