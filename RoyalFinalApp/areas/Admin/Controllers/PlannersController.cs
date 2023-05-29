using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models;
using RoyalFinalApp.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoyalFinalApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class PlannersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlannersController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment=webHostEnvironment;
        }

        // GET: Admin/Planners
        public async Task<IActionResult> Index()
        {
              return _context.Planners != null ? 
                          View(await _context.Planners.Where(x=>x.IsPuplished==true&&x.IsDeleted==false).ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Planners'  is null.");
        }

        // GET: Admin/Planners/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Planners == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }

        // GET: Admin/Planners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Planners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlannerViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                string imgName = FileUpload(model);

                Planner planner = new Planner
                {
                    Id = model.Id,
                    PlannerName=model.PlannerName,
                    PlannerImg=imgName,
                    Specialist=model.Specialist,
                    IsDeleted=model.IsDeleted,
                    IsPuplished=model.IsPuplished,
                    
                };
                _context.Add(planner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/Planners/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Planners == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners.FindAsync(id);
            if (planner == null)
            {
                return NotFound();
            }
            PlannerViewModel model = new PlannerViewModel
            {
                Id=planner.Id,
                PlannerName=planner.PlannerName,
                Specialist=planner.Specialist,
                IsDeleted=planner.IsDeleted,
                IsPuplished=planner.IsPuplished,
                Image=null,               
            };
            return View(model);
        }

        // POST: Admin/Planners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,PlannerViewModel model)
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

                    Planner planner = new Planner
                    {
                        Id = model.Id,
                        PlannerName=model.PlannerName,
                        PlannerImg=imgName,
                        Specialist=model.Specialist,
                        IsDeleted=model.IsDeleted,
                        IsPuplished=model.IsPuplished,

                    };
                    _context.Update(planner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlannerExists(model.Id))
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

        // GET: Admin/Planners/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Planners == null)
            {
                return NotFound();
            }

            var planner = await _context.Planners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planner == null)
            {
                return NotFound();
            }

            return View(planner);
        }

        // POST: Admin/Planners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Planners == null)
            {
                return Problem("Entity set 'AppDbContext.Planners'  is null.");
            }
            var planner = await _context.Planners.FindAsync(id);
            if (planner != null)
            {
                _context.Planners.Remove(planner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlannerExists(Guid id)
        {
          return (_context.Planners?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public string FileUpload(PlannerViewModel model)
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

        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var data = _context.Planners.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsDeleted = true;
                _context.Planners.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlannerExists(data!.Id))
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
        public IActionResult AllHide()
        {
            var result = _context.Planners.Where(x => x.IsDeleted==true).ToList();
            return View("Index", result);
        }
        public async Task<IActionResult> RestoreSoftDelete(Guid id)
        {
            var data = _context.Planners.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsDeleted = false;
                _context.Planners.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlannerExists(data!.Id))
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
        public async Task<IActionResult> UnPublishedList()
        {
            var result =await  _context.Planners.Where(x => x.IsPuplished == false).ToListAsync();
            return View("Index", result);
        }
        public async Task<IActionResult> Publish(Guid id)
        {
            var data = _context.Planners.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsPuplished = true;
                _context.Planners.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlannerExists(data!.Id))
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
        public async Task<IActionResult> UnPublish(Guid id)
        {
            var data = _context.Planners.Find(id);
            if (id != data!.Id)
            {
                return NotFound();
            }
            try
            {
                data.IsPuplished = false;
                _context.Planners.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlannerExists(data!.Id))
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
