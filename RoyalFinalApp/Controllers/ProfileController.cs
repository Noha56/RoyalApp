using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using RoyalFinalApp.Models.ViewModels;

namespace RoyalFinalApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IToastNotification _toastNotification;

        public ProfileController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IToastNotification toastNotification)
        {
            _signInManager= signInManager;
            _userManager= userManager;
            _toastNotification= toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage("You are not registered yet!");
                    return RedirectToAction("Login","Account");
                }

                // ChangePasswordAsync changes the user password
                var result = await _userManager.ChangePasswordAsync(user,
                    model.CurrentPassword!, model.NewPassword!);

                // The new password did not meet the complexity rules or
                // the current password is incorrect. Add these errors to
                // the ModelState and rerender ChangePassword view
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                // Upon successfully changing the password refresh sign-in cookie
                await _signInManager.RefreshSignInAsync(user);
                _toastNotification.AddSuccessToastMessage("Your password has been changed successfully!");

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
