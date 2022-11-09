using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.ViewModels;

namespace TaskManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email,
            };
            var result = await userManager.CreateAsync(user: user, password: model.Password);
            if(result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }else
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return View(model);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        #endregion

    }
}
