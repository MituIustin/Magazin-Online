﻿using Microsoft.AspNetCore.Authorization;
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

        public IActionResult ProductReview(Product product)
        {

            var reviews = product.Reviews;

            int _perPage = 3;

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

            return PartialView();


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
                Product prod = db.Products.Find(prodid);
                

                db.Reviews.Add(review);
                db.SaveChanges();

                var stars = GetStars(prod.ProductId);
                prod.rating = stars;
                db.SaveChanges();

                TempData["message"] = "Reviewl a fost adaugat cu succes.";
                return RedirectToAction("Show", "Product", new { id = prodid, section="review" ,page=1});
            }
            catch (Exception)
            {
                TempData["message"] = "A aparut o eroare la adaugarea review-lui.";
                return RedirectToAction("Show", "Product", new { id = prodid, section = "review",page=1 });
            }

        }


        public IActionResult Delete(int id)
        {

            var reviewToDelete = db.Reviews.Find(id);

            var prodid = reviewToDelete.ProductId;




            // Check if the user is authorized to delete the comment
            if (reviewToDelete != null && (User.IsInRole("Admin") || reviewToDelete.UserId == _userManager.GetUserId(User)))
            {
                db.Reviews.Remove(reviewToDelete);
                db.SaveChanges();

                TempData["message"] = "Review-ul a fost sters cu succes.";

                var product = db.Products.Find(prodid);
                var stars=GetStars((int)prodid);
                product.rating = stars; 
                db.SaveChanges();

            }
            else
            {
                TempData["message"] = "Nu s-a putut sterge review-ul.";
            }

            return RedirectToAction("Show", "Product", new { id = prodid, section = "review", page =1});
        }

        public IActionResult Edit(int id)
        {
            var mia = db.Reviews.Find(id);
            Review review = new Review();
            review.ReviewId = id;
            review.ProductId = mia.ProductId;
            
            return View(review);
        }

        [HttpPost]
        public IActionResult Edit(Review model)
        {
            if (ModelState.IsValid)
            {
                Review review = db.Reviews.Find(model.ReviewId);
                
                
                if (review == null)
                {
                    TempData["message"] = "Review-ul nu a fost gasit.";
                    TempData["messageType"] = "alert-danger";
                    return RedirectToAction("Show", "Product", new {id=review.ProductId, section = "review",page=1 });
                }

                

                try
                {
                    review.Description = model.Description;
                    review.Rating = model.Rating;
                    db.SaveChanges();

                    var product = db.Products.Find(model.ProductId);
                    var stars = GetStars((int)model.ProductId);
                    product.rating = stars;
                    db.SaveChanges();


                    TempData["message"] = "Review-ul a fost modificat cu succes.";
                    TempData["messageType"] = "alert-success";
                    return RedirectToAction("Show", "Product", new { id = model.ProductId, section = "review", page=1 });
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
        [NonAction]
        public float GetStars(int id)
        {
            float rating = 0;
            float count = 0;
            var reviews = db.Reviews.Where(r => r.ProductId == id);

            foreach (var review in reviews)
            {
                rating = review.Rating + rating;
                count++;
            }

            if (count == 0)
            {
                return 0;
            }
            else
            {
                return rating / count;
            }



            /*
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
            */
            return 0;

        }


    }
}

    
