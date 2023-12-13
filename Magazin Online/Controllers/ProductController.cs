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
            int _perPage = 3;

            var products = db.Products.Include("User");
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message =
                TempData["message"].ToString();
                ViewBag.Alert = TempData["messageType"];
            }

            int total_products=products.Count();
            var currentPage =Convert.ToInt32(HttpContext.Request.Query["page"]);
            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * _perPage;
            }

            var paginatedProducts = products.Skip(offset).Take(_perPage);

            ViewBag.lastPage = Math.Ceiling((float)total_products/(float)_perPage);

            ViewBag.Products = paginatedProducts;


            return View();
        }
        public IActionResult Show(int id)
        {
            Product product = db.Products.Include("Category")
                                         .Include("User")
                                         .Include("Comments")
                                         .Include("Comments.User")
                                         .Where(prod => prod.ProductId == id)
                                         .First();


            SetAccessRights();

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            return View(product);
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
        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Editor"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }
    }
}
