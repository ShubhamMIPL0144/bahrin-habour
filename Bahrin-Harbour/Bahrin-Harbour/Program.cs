using Bahrin.Harbour.Data.ClientDA;
using Bahrin.Harbour.Data.DataContext;
using Bahrin.Harbour.Data.DBCollections;
using Bahrin.Harbour.Model.AccountModel;
using Bahrin.Harbour.Service.AccoutService;
using Bahrin.Harbour.Service.AppUserService;
using Bahrin.Harbour.Service.ClientService;
using Bahrin.Harbour.Service.EmailService;
using Bahrin.Harbour.Service.UserAccountService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Bahrin.Harbour.Model.EmailModel.EmailModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); ;

builder.Services.AddRazorPages();

builder.Services.AddDbContext<BahrinHarbourContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("Bahrin.Harbour.Data")));

builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));

var sessionSettings = builder.Configuration.GetSection("Session");
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.Parse(sessionSettings.GetValue<string>("IdleTimeout"));
    options.Cookie.HttpOnly = sessionSettings.GetValue<bool>("CookieHttpOnly");
    options.Cookie.IsEssential = sessionSettings.GetValue<bool>("CookieIsEssential");
});

var authSettings = builder.Configuration.GetSection("Authentication:Cookie");
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = authSettings.GetValue<string>("Name");
        options.LoginPath = authSettings.GetValue<string>("LoginPath");
        options.LogoutPath = authSettings.GetValue<string>("LogoutPath");
        options.ExpireTimeSpan = TimeSpan.Parse(authSettings.GetValue<string>("ExpireTimeSpan"));
        options.SlidingExpiration = authSettings.GetValue<bool>("SlidingExpiration");
    });




////
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BahrinHarbourContext>().AddDefaultTokenProviders();
//////
///Dipendency Injection
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<IClientDA, ClientDA>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<IImageService, ImageService>();



/////
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeed.Initialize(services);
}
app.UseHttpsRedirection();
app.UseStaticFiles();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});*/
app.UseRouting();

app.UseSession();

app.MapDefaultControllerRoute();

app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{area=administration}/{controller=Account}/{action=SignIn}/{id?}");*/

app.MapControllerRoute(
     name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
