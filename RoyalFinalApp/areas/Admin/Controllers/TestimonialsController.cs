using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;

namespace RoyalFinalApp.areas.Admin.Controllers
{
    [Area("Admin")]
	//[Authorize(Roles = "Admin")]
	public class TestimonialsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public TestimonialsController(AppDbContext context, UserManager<IdentityUser> userManager
            , IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        // GET: Testimonials
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return _context.Testimonials != null ?
                        View(await _context.Testimonials.Where(x => x.IsDeleted==false&&x.IsPuplished==true).ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Testimonials'  is null.");
        }

        // GET: Testimonials/Details/5
        [Authorize(Roles = "Admin")]
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



        // GET: Testimonials/Edit/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, Testimonial testimonial)
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "User")]
		public IActionResult WriteReview()
        {
            return View();
        }
        [HttpPost]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> WriteReview(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.IsDeleted = false;
                testimonial.IsPuplished = false;
                testimonial.Status=false;

                _context.Add(testimonial);
                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("Thank you, for your review");
                return RedirectToAction(nameof(WriteReview));
            }
            return View(testimonial);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var data = _context.Testimonials.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsDeleted = true;
                _context.Testimonials.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(data!.Id))
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
        [Authorize(Roles = "Admin")]
        public IActionResult AllHide()
        {
            var result = _context.Testimonials.Where(x => x.IsDeleted==true).ToList();
            return View("Index",result);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RestoreSoftDelete(Guid id)
        {
            var data = _context.Testimonials.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsDeleted = false;
                _context.Testimonials.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(data!.Id))
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
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UnPublishedList()
        {
            var result = await _context.Testimonials.Where(x => x.IsPuplished == false).ToListAsync();
            return View("Index", result);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Publish(Guid id)
        {
            var data = _context.Testimonials.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsPuplished = true;
                _context.Testimonials.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(data!.Id))
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnPublish(Guid id)
        {
            var data = _context.Testimonials.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsPuplished = false;
                _context.Testimonials.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestimonialExists(data!.Id))
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

    }
}
