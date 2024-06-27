using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApp.DataStore.Interfaces;
using WebApp.DataStore.SQL;
using WepApp.DataStore.Interfaces;
using WepApp.DataStore.SQL;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("_config.json");

builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddScoped<IUsersRepository, UsersSQLRepository>();
builder.Services.AddScoped<IRolesRepository, RolesSQLRepository>();
builder.Services.AddScoped<IResumesRepository, ResumesSQLRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesSQLRepository>();
builder.Services.AddScoped<ILocationsRepository, LocationsSQLRepository>();
builder.Services.AddScoped<IEmployersRepository, EmployersSQLRepository>();
builder.Services.AddScoped<IJobsRepository, JobsSQLRepository>();
builder.Services.AddScoped<IJobApplicationsRepository, JobApplicationsSQLRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("candidates", p => p.RequireClaim("role", "candidate"));
    opt.AddPolicy("employers", p => p.RequireClaim("role", "employer"));
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
