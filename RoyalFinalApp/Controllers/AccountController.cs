using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoyalFinalApp.Models.ViewModels;
using System.Data;

namespace RoyalFinalApp.Controllers
{
    public class AccountController : Controller
    {
        #region Configration
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;


        //private ILogger<AccountController> _logger;
        //private IEmailSender _emailSender;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;   
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
                    RedirectToAction("Login");
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
                    return RedirectToAction("Index", "Home");

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

        #region Roles
        // [Authorize(Roles ="Admin")]
        public IActionResult CreateRole()
        {

            return View();
        }
        [HttpPost]
        //   [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name=model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]

        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
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
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name!))
                {
                    model.Users!.Add(user.UserName!);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId!);
                if (role == null)
                {
                    return RedirectToAction(nameof(ErrorPage));
                }
                role.Name=model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(RolesList));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);

            }
            return View(model);
        }
        public IActionResult ErrorPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ModifyUserInRole(string id)
        {
            if (id==null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction(nameof(ErrorPage));
            }
            List<RoleViewModel> models = new List<RoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                RoleViewModel userRole = new RoleViewModel
                {
                    Id= user.Id,
                    Name= user.UserName,
                };
                if (await _userManager.IsInRoleAsync(user, role.Name!))

                {
                    userRole.IsSelected= true;
                }
                else
                {
                    userRole.IsSelected=false;
                }
                models.Add(userRole);
            }
            return View(models);
        }
        [HttpPost]
        public async Task<IActionResult> ModifyUsersInRole(string id, List<RoleViewModel> models)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(RolesList));
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction(nameof(ErrorPage));
            }
            IdentityResult result = new IdentityResult();
            for (int i = 0; i < models.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(models[i].Id!);
                if (models[i].IsSelected && (!await _userManager.IsInRoleAsync(user!, role.Name!)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name!);
                }
                else if (!models[i].IsSelected && (await _userManager.IsInRoleAsync(user!, role.Name!)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name!);
                }

            }
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(RolesList));
            }
            return View(models);

        }




        #endregion
    }
}
