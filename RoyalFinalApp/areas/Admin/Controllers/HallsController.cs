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
using RoyalFinalApp.Models.ViewModels;

namespace RoyalFinalApp.areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HallsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HallsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
           _webHostEnvironment=webHostEnvironment;
        }

        // GET: Admin/Halls
        public async Task<IActionResult> Index()
        {
            // var finalDbContext = _context.Halls.Include(c => c.Category);

            return _context.Halls != null ?
                        View(await _context.Halls.Include(c => c.Category).ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Halls'  is null.");
        }

        // GET: Admin/Halls/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View(hall);
        }

        // GET: Admin/Halls/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HallViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                Hall hall = new Hall
                {
                    Address= model.Address,
                    AvailableDate= DateTime.Now,
                    Capacity= model.Capacity,
                    CategoryId= model.CategoryId,
                    HallName = model.HallName,
                    Id = model.Id,
                    Status=model.Status,
                    IsDeleted=model.IsDeleted,
                    IsPuplished = model.IsPuplished,
                    Price=model.Price,
                    Image=imgName,
                    Category=model.Category,
                    IsBooked=model.IsBooked,
                    IsAvailable=model.IsAvailable,
                };

                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);

            return View(model);
        }

        // GET: Admin/Halls/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            HallViewModel model = new HallViewModel
            {
                Address= hall.Address,
                AvailableDate= hall.AvailableDate,
                Capacity= hall.Capacity,
                CategoryId= hall.CategoryId,
                HallName = hall.HallName,
                Id = hall.Id,
                Status=hall.Status,
                IsDeleted=hall.IsDeleted,
                IsPuplished = hall.IsPuplished,
                Price=hall.Price,
                Image=null,
                IsAvailable=hall.IsAvailable,
                IsBooked=hall.IsBooked,
                Category=hall.Category, 
            };
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", model.CategoryId);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HallViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string imgName = FileUpload(model);

                    Hall hall = new Hall
                    {
                        Address= model.Address,
                        AvailableDate= DateTime.Now,
                        Capacity= model.Capacity,
                        CategoryId= model.CategoryId,
                        HallName = model.HallName,
                        Id = model.Id,
                        Status=model.Status,
                        IsDeleted=model.IsDeleted,
                        IsPuplished = model.IsPuplished,
                        Price=model.Price,
                        Image=imgName,
                        Category=model.Category,
                        IsBooked=model.IsBooked,
                        IsAvailable=model.IsAvailable,
                    };

                    _context.Update(hall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(model.Id))
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
            return View(model);
        }
        // GET: Admin/Halls/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }
        // POST: Admin/Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Halls == null)
            {
                return Problem("Entity set 'AppDbContext.Halls'  is null.");
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(Guid id)
        {
            return (_context.Halls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public string FileUpload(HallViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "Images");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Image!.FileName!);
            string newImgName = "Roral_"+ fileName +"_"+
                Guid.NewGuid().ToString()+Path.GetExtension(model.Image.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Image.CopyTo(fileStream);
            }
            return "\\Images\\"+ newImgName;
        }
    }
}
