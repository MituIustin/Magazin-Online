using Microsoft.AspNetCore.Mvc;

namespace Magazin_Online.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
