using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;
using System.Diagnostics;

namespace RoyalFinalApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
       private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.status=false;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Search()
        {

            return View("Index");
        }

        [HttpPost]
        public IActionResult Search(string input)
        {
            if (input==null)
            {
                return RedirectToAction(nameof(Index));
            }
            var result =_db.Halls.Where(x=>x.HallName==input||x.Category!.CategoryName==input).ToList();
            ViewBag.status=true;
            //  return PartialView("HallView",result);  
            return View("Index", result);
        }
        public IActionResult SoonPage()
        {
            return View();
        }
    }
}