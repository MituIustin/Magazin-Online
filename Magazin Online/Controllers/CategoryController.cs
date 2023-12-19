using System.Data;
using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Online.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CategoryController(
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
            var categories = from category in db.Categories
                             select category;
            ViewBag.Categories = categories;
            return View();
        }
        [Authorize(Roles = "Admin")]

        public IActionResult New()
        {
            Category category = new Category();
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult New(Category c)
        {
            try
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);

            var productsWithCategory = db.Products.Where(p => p.CategoryId == id).ToList();

            if (productsWithCategory.Any())
            {
                TempData["message"] = "Nu puteti sterge aceasta categorie deoarece exista produse cu categoria respectiva.";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Product");
            }


            if (User.IsInRole("Admin"))
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                TempData["message"] = "Categoria a fost stersa";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index","Category");
            }

            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti aceasta categorie.";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Category");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Găsește categoria după id
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                TempData["message"] = "Categoria nu a fost gasita.";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Category");
            }

            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                Category category = db.Categories.Find(model.CategoryId);

                if (category == null)
                {
                    TempData["message"] = "Categoria nu a fost gasita.";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Index", "Category");
                }

                category.Name = model.Name;

                try
                {
                    db.SaveChanges();

                    TempData["message"] = "Categoria a fost modificata cu succes.";
                    TempData["messageType"] = "alert-success";
                    return RedirectToAction("Index", "Category");
                }
                catch (Exception)
                {
                    TempData["message"] = "A aparut o eroare la salvarea modificarilor.";
                    TempData["messageType"] = "alert-danger";
                    return View(model);
                }
            }

            return View(model);
        }



    }
}
