using App.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();
services.AddRazorPages();
services.AddDbContext<AppDbContext>(options => {
    string? connectString = config.GetConnectionString("TravelokaContext");
    options.UseSqlServer(connectString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

// StatusCodePages khi không có Route nào xử lý.
app.UseStatusCodePages();
app.Run();
