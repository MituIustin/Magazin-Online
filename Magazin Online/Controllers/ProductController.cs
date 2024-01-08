using System.Data;
using System.IO;
using System.Linq.Expressions;
using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

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

        
        



        public IActionResult Index(string? searched)
        {

            ViewBag.searched = searched;
            var tosort = Convert.ToString(HttpContext.Request.Query["sort"]);

            ViewBag.sort = tosort;


            var products = db.Products.Include("User").Where(p => p.IsAccepted == true);
            if (searched != null)
            {
                products = products.Where(p => p.Title.Contains(searched));
            }
            
            foreach (var product in products)
            {
                var stars = GetStars(product.ProductId);
                product.rating = stars;
            }
            int _perPage = 3;
            
            if(ViewBag.sort=="pcresc")
            {
                products = products.OrderBy(p => p.Price);
            }
            else if (ViewBag.sort == "pdesc")
            {
                products = products.OrderByDescending(p => p.Price);
            }
            

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

        public IActionResult pretcresc(string? searched)
        {
            TempData["sort"] = "pcresc";
            return RedirectToAction("Index", new {page=1, searched=searched, sort="pcresc"});
        }
        public IActionResult pretdescresc(string? searched)
        {
            TempData["sort"] = "pdesc";
            return RedirectToAction("Index", new {page=1, searched = searched, sort = "pdesc" });
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

            var stars = GetStars(product.ProductId);
            product.rating = stars;

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
                TempData["message"] = "Produsul a fost sters";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un produs care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Contributor,Admin")]
        public IActionResult Edit(int id)
        {

            Product product = db.Products.Include("Category")
                                        .Where(prod => prod.ProductId == id)
                                        .First();

            product.Categ = GetAllCategories();

            if (product.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            {
                return View(product);
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui produs care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Contributor,Admin")]
        public IActionResult Edit(int id, Product requestProduct)
        {
            Product product = db.Products.Find(id);


            if (ModelState.IsValid)
            {
                if (product.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
                {
                    product.Title = requestProduct.Title;
                    product.Description = requestProduct.Description;
                    product.CategoryId = requestProduct.CategoryId;
                    product.Price=requestProduct.Price;
                    TempData["message"] = "Produsul a fost modificat";
                    TempData["messageType"] = "alert-success";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "Nu aveti dreptul sa faceti modificari asupra unui produs care nu va apartine";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                requestProduct.Categ = GetAllCategories();
                return View(requestProduct);
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
        [NonAction]
        public float GetStars(int id)
        {
            float rating = 0;
            float count = 0;
            var reviews = db.Products.Find(id).Reviews;
            if (reviews == null)
            {
                return 0;
            }
            foreach (var review in reviews)
            {
                rating = rating + review.Rating;
                count++;
            }
            
            return rating / count;
        }


    }
}
