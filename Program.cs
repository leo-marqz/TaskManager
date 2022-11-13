using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskManager;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

//policies
var policyForAuthenticatedUsers = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter(policyForAuthenticatedUsers));
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(SharedResource));
    });

builder.Services.AddRouting(router => router.LowercaseUrls = true);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");
});
//activate identity -----------------------------------------------------------
builder.Services.AddAuthentication()
    .AddMicrosoftAccount(options =>
    {
        options.ClientId = builder.Configuration["MicrosoftClientId"];
        options.ClientSecret = builder.Configuration["MicrosoftSecretId"];
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.PostConfigure<CookieAuthenticationOptions>(
    IdentityConstants.ApplicationScheme, options =>
    {
        options.LoginPath = "/users/login";
        options.AccessDeniedPath = "/users/login";
    }
    );

//localization
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

//-------------------------------------------------
var app = builder.Build();

//Languages
//var UICultures = new[] { "es", "en" };

app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture("es");
    options.SupportedUICultures = ConstantsService.supportedUICultures
        .Select(culture => new CultureInfo(culture.Value)).ToList();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//active identity
app.UseAuthentication();

app.UseAuthorization();

//---------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
