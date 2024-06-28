using Microsoft.EntityFrameworkCore;
using Task5.DataStore;
using WepApp.DataStore.SQL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("_config.json");

builder.Services.AddDbContext<ApplicationContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"));
});

builder.Services.AddScoped<ProjectRepository, ProjectRepository>();
builder.Services.AddScoped<RisksRepository, RisksRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Projects}/{action=Index}/{id?}");

app.Run();
