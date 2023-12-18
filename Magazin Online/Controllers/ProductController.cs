using System.Data;
using System.IO;
using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var products = db.Products.Include("User").Where(p=> p.IsAccepted==true);
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




        [Authorize(Roles = "Contributor,Admin")]
        public IActionResult New()
        {
            Product product = new Product();

            product.Categ = GetAllCategories();
            return View(product);
        }

        [Authorize(Roles = "Contributor,Admin")]
        [HttpPost]
        public async Task<IActionResult> New(Product product, IFormFile file)
        {
            product.UserId = _userManager.GetUserId(User);
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            product.Photo = stream.ToArray();
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            if (ModelState.IsValid && product.Photo != null)
            {
                
                if (User.IsInRole("Admin"))
                {
                    product.IsAccepted = true;
                }
                else
                {
                    product.IsAccepted = false;
                }
                db.Products.Add(product);
                db.SaveChanges();
                TempData["message"] = "Produsul a fost adaugat";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                product.Categ = GetAllCategories();

                return View(product);
            }
        }

        public IActionResult Accept(int id)
        {
            Product product=db.Products.Find(id);
            product.IsAccepted=true;
            db.SaveChanges();
            return RedirectToAction("Index", "Request");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Include("Comments")
                                         .Where(p => p.ProductId == id)
                                         .First();

            if (product.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                db.Products.Remove(product);
                db.SaveChanges();
                TempData["message"] = "Articolul a fost sters";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un articol care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }
        private void SetAccessRights()
        {
            ViewBag.AfisareButoane = false;

            if (User.IsInRole("Contributor"))
            {
                ViewBag.AfisareButoane = true;
            }

            ViewBag.EsteAdmin = User.IsInRole("Admin");

            ViewBag.UserCurent = _userManager.GetUserId(User);
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            var selectList = new List<SelectListItem>();

            var categories = from cat in db.Categories
                             select cat;

            foreach (var category in categories)
            {
                
                selectList.Add(new SelectListItem()
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.Name.ToString()
                }); 
            }
            return selectList;
        }


    }
}
