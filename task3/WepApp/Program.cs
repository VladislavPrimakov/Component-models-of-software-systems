using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WepApp.DataStore.Interfaces;
using WepApp.DataStore.SQL;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("_config.json");

builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddScoped<IPlacementRepository, PlacementSQLRepository>();
builder.Services.AddScoped<IProductRepository, ProductSQLRepository>();
builder.Services.AddScoped<IUserRepository, UserSQLRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("users", p => p.RequireClaim("role", "user"));
    opt.AddPolicy("admins", p => p.RequireClaim("role", "admin"));
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
