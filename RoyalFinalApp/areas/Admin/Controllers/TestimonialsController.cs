using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;

namespace RoyalFinalApp.areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TestimonialsController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
              return _context.Testimonials != null ? 
                          View(await _context.Testimonials.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Testimonials'  is null.");
        }

        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,Testimonial testimonial)
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Testimonials == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Testimonials == null)
            {
                return Problem("Entity set 'AppDbContext.Testimonials'  is null.");
            }
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool TestimonialExists(Guid id)
        {
          return (_context.Testimonials?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult WriteReview()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> WriteReview(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.IsDeleted = false;
                testimonial.IsPuplished = false;
                testimonial.Status=false;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //testimonial.UserId=userId;
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }
    }
}
