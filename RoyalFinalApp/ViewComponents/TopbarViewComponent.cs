using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Data;

namespace RoyalFinalApp.ViewComponents
{
    public class TopbarViewComponent:ViewComponent
    {
        private  AppDbContext _db;
        public TopbarViewComponent(AppDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.Topbars.FirstOrDefault();
            return View(data);
        }
    }
}
