using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;
using RoyalFinalApp.Models.ViewModels;

namespace RoyalFinalApp.areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AboutUsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment=webHostEnvironment;
        }

        // GET: AboutUs
        public async Task<IActionResult> Index()
        {
              return _context.AboutUs != null ? 
                          View(await _context.AboutUs.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.AboutUs'  is null.");
        }

        // GET: AboutUs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // GET: AboutUs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                model.Id = Guid.NewGuid();

                AboutUs aboutUs = new AboutUs
                {
                    Id= model.Id,
                    Description=model.Description,
                    Image=imgName,
                    Title=model.Title,
                };
                _context.Add(aboutUs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: AboutUs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs.FindAsync(id);

            AboutUsViewModel model = new AboutUsViewModel
            {
                Id = aboutUs.Id,
                Description = aboutUs.Description,
                Title = aboutUs.Title,
            };

            if (aboutUs == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: AboutUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,AboutUsViewModel model)
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

                    AboutUs aboutUs = new AboutUs
                    {
                        Id= model.Id,
                        Description=model.Description,
                        Image=imgName,
                        Title=model.Title,
                    };


                    _context.Update(aboutUs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutUsExists(model.Id))
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
            return View(model); ;
        }

        // GET: AboutUs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AboutUs == null)
            {
                return NotFound();
            }

            var aboutUs = await _context.AboutUs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutUs == null)
            {
                return NotFound();
            }

            return View(aboutUs);
        }

        // POST: AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AboutUs == null)
            {
                return Problem("Entity set 'AppDbContext.AboutUs'  is null.");
            }
            var aboutUs = await _context.AboutUs.FindAsync(id);
            if (aboutUs != null)
            {
                _context.AboutUs.Remove(aboutUs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutUsExists(Guid id)
        {
          return (_context.AboutUs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public string FileUpload(AboutUsViewModel model)
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
