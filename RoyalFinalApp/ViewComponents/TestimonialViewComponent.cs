using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Data;

namespace RoyalFinalApp.ViewComponents
{
    public class TestimonialViewComponent:ViewComponent
    {
        private AppDbContext _db;
        public TestimonialViewComponent(AppDbContext db)
        {
            _db=db;
        }

        public IViewComponentResult Invoke()
        {
            var data = _db.Testimonials.OrderByDescending(x => x.Id);
            return View(data);
        }
    }
}
