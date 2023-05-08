using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Data;

namespace RoyalFinalApp.ViewComponents
{
    public class HallViewComponent:ViewComponent
    {

        private AppDbContext _db;
        public HallViewComponent(AppDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.Halls.OrderByDescending(x => x.Id);
            return View(data);
        }
    }
}
