using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using preciousportfolio.Data;
using preciousportfolio.Services;
using QuestPDF.Infrastructure;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Configure QuestPDF license once at startup
QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "preciousportfolio.db");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<ISpotPriceService, GoldApiSpotPriceService>()
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        UseProxy = false
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();