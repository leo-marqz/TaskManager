using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var err in result.Errors)
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
        public IActionResult Login(string message = null)
        {
            if (message is not null)
            {
                ViewData["message"] = message;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(
                userName: model.Email,
                password: model.Password,
                isPersistent: model.RememberMe,
                lockoutOnFailure: false
                );
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email (usuario) o contraseña son invalidos");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ChallengeResult ExternalLogin(string provider, string theReturnUrl = null)
        {
            var redirectUrl = Url.Action("RegisterExternalUser", values: new { theReturnUrl });
            var properties = signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterExternalUser(
            string theReturnUrl = null, string remoteError = null
            )
        {
            var redirectUrl = theReturnUrl ?? Url.Content("~/");
            var message = "";
            if (remoteError is not null)
            {
                message = $"Error del proveedor externo: {remoteError}";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info is null)
            {
                message = "Error cargando la información de inicio de sesión externo";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var resultExternalLogin = await signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true
                );
            if (resultExternalLogin.Succeeded)
            {
                return LocalRedirect(redirectUrl);
            }

            string email = "";
            if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                email = info.Principal.FindFirstValue(ClaimTypes.Email);
            }
            else
            {
                message = "Error leyendo el email del usuario del proveedor";
                return RedirectToAction("Login", routeValues: new { message });
            }

            var user = new IdentityUser { Email = email, UserName = email };
            var resultCreateUser = await userManager.CreateAsync(user);

            if (!resultCreateUser.Succeeded)
            {
                message = resultCreateUser.Errors.First().Description;
                return RedirectToAction("Login", routeValues: new { message });
            }

            var resultAddLogin = await userManager.AddLoginAsync(user, info);
            if (resultAddLogin.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true, info.LoginProvider);
                return LocalRedirect(redirectUrl);
            }
            message = "Ha ocurrido un error en el inicio de sesión";
            return RedirectToAction("Login", routeValues: new { message });

        }

        #endregion

    }
}
