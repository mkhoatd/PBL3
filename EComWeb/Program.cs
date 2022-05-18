using System.Globalization;
using ECom.DataAccess.Data;
using ECom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ECom.Utility;
using Microsoft.AspNetCore.Localization;
using NuGet.Common;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();



// Add services to the container.;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeAreaFolder("Management", "/");
});
builder.Services.Configure<IdentityOptions>(options=>
{
    //Password settings
    options.Password.RequireDigit=false;
    options.Password.RequireLowercase=false;
    options.Password.RequireUppercase=false;
    options.Password.RequiredLength=6;
    options.Password.RequireNonAlphanumeric=false;

    //Lockout settings
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromDays(1);
    options.Lockout.MaxFailedAccessAttempts=10;
    options.Lockout.AllowedForNewUsers=true;

    //user settings
    options.User.AllowedUserNameCharacters=
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
    options.User.RequireUniqueEmail=true;
});
builder.Services.ConfigureApplicationCookie(options=>
{
    //Cookie settings
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await ApplicationDbContextSeedData.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

// app.UseRequestLocalization(new RequestLocalizationOptions()
// {
//     DefaultRequestCulture = new RequestCulture("en-GB"),
//     SupportedCultures=new List<CultureInfo>()
//     {
//         new CultureInfo("en-GB")
//     },
//     SupportedUICultures =new List<CultureInfo>()
//     {
//         new CultureInfo("en-GB")
//     }
// });

app.Run();
