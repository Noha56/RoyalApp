using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using RoyalFinalApp.Models.ViewModels;
using System.Data;

namespace RoyalFinalApp.Controllers
{

	public class AccountController : Controller
	{
		#region Configration
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IToastNotification _toastNotification;


		//private ILogger<AccountController> _logger;
		//private IEmailSender _emailSender;
		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
			RoleManager<IdentityRole> roleManager, IToastNotification toastNotification)
		{
			_userManager=userManager;
			_signInManager=signInManager;
			_roleManager=roleManager;
			_toastNotification=toastNotification;
		}

		#endregion

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				IdentityUser user = new IdentityUser
				{
					Email= model.Email,
					UserName=model.Email
				
				};
				var result = await _userManager.CreateAsync(user, model.Password!);
                await _userManager.AddToRoleAsync(user,"User");

                if (result.Succeeded)
				{

				
					_toastNotification.AddSuccessToastMessage("You have been registered successfully");
					return RedirectToAction("Login");
				}
				foreach (var err in result.Errors)
				{
					ModelState.AddModelError(err.Code, err.Description);
				}

				//1c55909f-1b8c-4f5e-9a88-c9029fd4297d
				return View(model);
			}
			return View(model);
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!, false, false);
				if (result.Succeeded)
				{ 
                    if (User.IsInRole("User"))
					{
						return RedirectToAction("Index", "Home");
					}
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    return RedirectToAction("Register", "Account");
                }
				ModelState.AddModelError("", "Invalid Email or Password");
				return View(model);
			}
			return View(model);
		}
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}
		
    }
}
