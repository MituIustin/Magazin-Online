using System.Data;
using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Online.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ProductController(
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
            //var products = db.Product.Include("Category").Include("User");

            // ViewBag.OriceDenumireSugestiva
            //ViewBag.Articles = products;
            
            return View();
        }

        [Authorize(Roles = "Editor,Admin")]
        public IActionResult New()
        {
            Product product = new Product();

            return View(product);
        }

        [Authorize(Roles = "Editor,Admin")]
        [HttpPost]
        public IActionResult New(Product product)
        {
            // preluam id-ul utilizatorului care posteaza articolul

            product.UserId = _userManager.GetUserId(User);


            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                TempData["message"] = "Produsul a fost adaugat";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

    }
}
