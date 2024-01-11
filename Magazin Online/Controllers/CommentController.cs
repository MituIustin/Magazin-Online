using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Magazin_Online.Controllers
{
    public class CommentController : Controller
    {

        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public CommentController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        public IActionResult New(int prod_id)
        {
            // Pass the productId to the view
            ViewBag.ProductId = prod_id;
            TempData["id"]= prod_id;
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult New(Comment comment)
        {
            var prodid = TempData["id"];
            prodid = comment.ProductId;
            try
            {
                var currentUser = _userManager.GetUserAsync(User).Result;

                comment.UserId = currentUser.Id;
                comment.User = currentUser;

                db.Comments.Add(comment);
                db.SaveChanges();
                
                TempData["message"] = "Comentariul a fost adaugat cu succes.";
                return RedirectToAction("Show", "Product",new {id=prodid });
            }
            catch (Exception)
            {
                TempData["message"] = "A aparut o eroare la adaugarea comentariului.";
                return RedirectToAction("Show", "Product", new { id = prodid });
            }
        }


        public IActionResult Delete(int id)
        {
           
            var commentToDelete = db.Comments.Find(id);

            // Check if the user is authorized to delete the comment
            if (commentToDelete != null && (User.IsInRole("Admin") || commentToDelete.UserId == _userManager.GetUserId(User)))
            {
                db.Comments.Remove(commentToDelete);
                db.SaveChanges();

                TempData["message"] = "Comentariul a fost sters cu succes.";
            }
            else
            {
                TempData["message"] = "Nu s-a putut sterge comentariul.";
            }

            return RedirectToAction("Index", "Product", new { page = 1, sort = "norm", searched = (string)null });
        }

        public IActionResult Edit(int id)
        {
            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                TempData["message"] = "Comentariul nu a fost gasit.";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Product", new { page = 1, sort = "norm", searched = (string)null });
            }

            return View(comment);
        }

        [HttpPost]
        public IActionResult Edit(Comment model)
        {
            if (ModelState.IsValid)
            {
                Comment comment = db.Comments.Find(model.CommentId);

                if (comment == null)
                {
                    TempData["message"] = "Comentariul nu a fost gasit.";
                    TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index", "Product", new { page = 1, sort = "norm", searched = (string)null });
                }

                comment.Content = model.Content;

                try
                {
                    db.SaveChanges();

                    TempData["message"] = "Comentariul a fost modificat cu succes.";
                    TempData["messageType"] = "alert-success";
                    return RedirectToAction("Index", "Product", new { page = 1, sort = "norm", searched = (string)null });
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
