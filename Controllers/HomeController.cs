using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> stringLocalizer;

        public HomeController(
            ILogger<HomeController> logger,
            IStringLocalizer<HomeController> stringLocalizer
            )
        {
            _logger = logger;
            this.stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            ViewBag.Greetings = stringLocalizer["Bienvenidos a Task Manager"]; //return: good morning is(english)
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string urlRedirect)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(2) }
                );
            return LocalRedirect(urlRedirect);
        }
    }
}