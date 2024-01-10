using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Magazin_Online.Data;

namespace Magazin_Online.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public ReviewController(
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


        [Authorize(Roles = "Contributor,Admin,User")]
        public IActionResult New(int prod_id)
        {
            var review = new Review();
            ViewBag.ProductId = prod_id;
            TempData["id"] = prod_id;
            return PartialView("ReviewNew", review);
        }

        [HttpPost]
        public IActionResult New(Review review)
        {
            var prodid = review.ProductId;
            try
            {
                var currentUser = _userManager.GetUserAsync(User).Result;

                review.UserId = currentUser.Id;
                review.User = currentUser;

                db.Reviews.Add(review);
                db.SaveChanges();

                TempData["message"] = "Reviewl a fost adaugat cu succes.";
                return RedirectToAction("Show", "Product", new { id = prodid });
            }
            catch (Exception)
            {
                TempData["message"] = "A aparut o eroare la adaugarea reviewului.";
                return RedirectToAction("Show", "Product", new { id = prodid });
            }

        }
    }
}

    
