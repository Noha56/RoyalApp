using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Models.ViewModels;

namespace RoyalFinalApp.Controllers
{
    public class AccountController : Controller
    {
        #region Configration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        //private ILogger<AccountController> _logger;
        //private IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
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
               var result= await _userManager.CreateAsync(user,model.Password!);
                if(result.Succeeded)
                {
                    return Content("Done!!!");
                }
            foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
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
            if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email!, model.Password!,model.RememberMe,false);
                if (result.Succeeded)
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    // return RedirectToAction("Index", "Home", new { area = "Clint" });
                    return Content("user login done corectlly");

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
