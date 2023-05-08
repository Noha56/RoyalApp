using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Data;

namespace RoyalFinalApp.ViewComponents
{
    public class PlannerViewComponent:ViewComponent
    {
        private AppDbContext _db;
        public PlannerViewComponent(AppDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.Planners.OrderByDescending(x => x.Id);
            return View(data);
        }
    }
}
