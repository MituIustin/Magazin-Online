using Magazin_Online.Data;
using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Magazin_Online.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public RequestController(
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
            var requests= db.Products.Where(p => p.IsAccepted == false);
            ViewBag.Requests = requests;
            return View();
        }
    }
}
