using Microsoft.AspNetCore.Mvc;

namespace RoyalFinalApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
