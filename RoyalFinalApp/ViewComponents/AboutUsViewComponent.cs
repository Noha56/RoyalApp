using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Data;

namespace RoyalFinalApp.ViewComponents
{
    public class AboutUsViewComponent:ViewComponent
    {
        private AppDbContext _db;
        public AboutUsViewComponent(AppDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.AboutUs.FirstOrDefault();
            return View(data);
        }
    }
}
