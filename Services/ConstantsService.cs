using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Services
{
    public class ConstantsService
    {
        public const string RoleAdmin = "admin";
        public static readonly SelectListItem[] supportedUICultures = new SelectListItem[]
        {
            new SelectListItem {Value = "es", Text = "Español" },
            new SelectListItem {Value = "en", Text = "English" }
        };
    }
}
