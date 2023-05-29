using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;

namespace RoyalFinalApp.areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class TopbarsController : Controller
    {
        private readonly AppDbContext _context;

        public TopbarsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Topbars
        public async Task<IActionResult> Index()
        {
              return _context.Topbars != null ? 
                          View(await _context.Topbars.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Topbars'  is null.");
        }

        // GET: Topbars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Topbars == null)
            {
                return NotFound();
            }

            var topbar = await _context.Topbars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topbar == null)
            {
                return NotFound();
            }

            return View(topbar);
        }

        // GET: Topbars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Topbars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Topbar topbar)
        {
            if (ModelState.IsValid)
            {
                topbar.Id = Guid.NewGuid();
                _context.Add(topbar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topbar);
        }

        // GET: Topbars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Topbars == null)
            {
                return NotFound();
            }

            var topbar = await _context.Topbars.FindAsync(id);
            if (topbar == null)
            {
                return NotFound();
            }
            return View(topbar);
        }

        // POST: Topbars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Topbar topbar)
        {
            if (id != topbar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topbar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopbarExists(topbar.Id))
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
            return View(topbar);
        }

        // GET: Topbars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Topbars == null)
            {
                return NotFound();
            }

            var topbar = await _context.Topbars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topbar == null)
            {
                return NotFound();
            }

            return View(topbar);
        }

        // POST: Topbars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Topbars == null)
            {
                return Problem("Entity set 'AppDbContext.Topbars'  is null.");
            }
            var topbar = await _context.Topbars.FindAsync(id);
            if (topbar != null)
            {
                _context.Topbars.Remove(topbar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopbarExists(Guid id)
        {
          return (_context.Topbars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
