using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Online.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;

            _userManager = userManager;

            _roleManager = roleManager;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            var admini = _userManager.GetUsersInRoleAsync("Admin").Result;
            var colaboratori = _userManager.GetUsersInRoleAsync("Contributor").Result;
            var useri = _userManager.GetUsersInRoleAsync("User").Result;

            ViewBag.admini = admini;
            ViewBag.colaboratori = colaboratori;
            ViewBag.useri = useri;

            
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult GiveRole(string id,string rol)
        {
            
            var user_role = db.UserRoles.Where(r=>r.UserId==id).First();

            var new_role = db.Roles.Where(r => r.Name == rol).First().Id;

            db.Remove(user_role);
            db.SaveChanges();
            db.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = new_role,
                    UserId = id.ToString()
                });
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
