using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RoyalFinalApp.Data;
using RoyalFinalApp.Models.ViewModels;

namespace RoyalFinalApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IToastNotification _toastNotification;

        public RolesController(AppDbContext context,RoleManager<IdentityRole> roleManager, IToastNotification toastNotification)
        {
            _context = context;
            _roleManager= roleManager;
            _toastNotification= toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var roles=await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        // [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _roleManager.RoleExistsAsync(model.RoleName!))
                {
                    ModelState.AddModelError("Name", "Role is exists!");
                    return View(model);
                }

                IdentityRole role = new IdentityRole
                {
                    Name=model.RoleName,
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            if (id== null)
            {
                return RedirectToAction("RolesList");

            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleName = role.Name,
                RoleId= role.Id,
            };
            //foreach (var user in _userManager.Users)
            //{
            //    if (await _userManager.IsInRoleAsync(user, role.Name!))
            //    {
            //        model.Users!.Add(user.UserName!);
            //    }
            //}
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId!);
                if (role == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                role.Name=model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);

            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            if (id== null)
            {
                return RedirectToAction("Index");

            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            return View(role);
        }
        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    _toastNotification.AddErrorToastMessage("The role is not exist!");
                    return RedirectToAction(nameof(Index));
                }
                var result = await _roleManager.DeleteAsync(role);
                await _context.SaveChangesAsync();

                if (result.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("The role is deleted successfully");
                    return RedirectToAction(nameof(Index));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View();
        }
    }
}
