using System.Data;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
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

        public IActionResult Index()
        {

           
            ViewBag.sort = Convert.ToString(HttpContext.Request.Query["sort"]);

            var searched = Convert.ToString(HttpContext.Request.Query["searched"]);

            var products = db.Products.Include("User").Where(p => p.IsAccepted == true);
            if (searched != null)
            {
                products = products.Where(p => p.Title.Contains(searched));
                ViewBag.searched = searched;
            }
            
            
            int _perPage = 20;
            
            if(ViewBag.sort=="pcresc")
            {
                products = products.OrderBy(p => p.Price);
            }
            else if (ViewBag.sort == "pdesc")
            {
                products = products.OrderByDescending(p => p.Price);
            }
            else if(ViewBag.sort == "rcresc")
            {
                products = products.OrderBy(p => p.rating);
            }
            else if (ViewBag.sort == "rdesc")
            {
                products = products.OrderByDescending(p => p.rating);
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
            var test = searched;
            if (searched == null)
            {
                test = "";
            }
            return RedirectToAction("Index", new {page=1,  sort="pcresc",searched = test });
        }
        public IActionResult pretdescresc(string? searched)
        {
            return RedirectToAction("Index", new {page=1, sort = "pdesc", searched = searched });
        }

        public IActionResult Show(int id)
        {
            Product product = db.Products.Include("Category")
                                         .Include("User")
                                         .Include("Comments")
                                         .Include("Comments.User")
                                         .Include("Reviews")
                                         .Include("Reviews.User")
                                         .Where(prod => prod.ProductId == id)
                                         .First();


            SetAccessRights();

            ViewBag.section= Convert.ToString(HttpContext.Request.Query["section"]);

            var reviews=product.Reviews;

            int _perPage = 3;

            var currentUser = _userManager.GetUserAsync(User).Result;

            ViewBag.role = User.IsInRole("Admin");

            if (currentUser != null)
            {
                ViewBag.currentid = currentUser.Id;
                TempData["currentUser"] = currentUser.Id; ;
            }
                
            else
            {
                ViewBag.currentid = null;
                TempData["currentUser"] = null;
            }



            if (TempData.ContainsKey("message"))
            {
                ViewBag.message =
                TempData["message"].ToString();
                ViewBag.Alert = TempData["messageType"];
            }

            int total_reviews = reviews.Count();
            var currentPage = Convert.ToInt32(HttpContext.Request.Query["page"]);
            var offset = 0;

            if (!currentPage.Equals(0))
            {
                offset = (currentPage - 1) * _perPage;
            }

            var paginatedReviews = reviews.Skip(offset).Take(_perPage);

            ViewBag.lastPage = Math.Ceiling((float)total_reviews / (float)_perPage);

            ViewBag.Reviews = paginatedReviews;

            ViewBag.count = total_reviews;

            ViewBag.comscount = product.Comments.Count;


            
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
        [Authorize(Roles ="Admin")]
        public IActionResult Accept(int id)
        {
            Product product=db.Products.Find(id);
            product.IsAccepted=true;
            db.SaveChanges();
            return RedirectToAction("Index", "Request");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Contributor")]
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
                    return RedirectToAction("Show", new {id=product.ProductId, section="descriere"});
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
        public int GetProductCountForCategory(int categoryId)
        {
            return db.Products.Count(p => p.CategoryId == categoryId);
        }

    }
    }
