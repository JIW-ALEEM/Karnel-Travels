using Karnel_Travels.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<KarnelTravelContext>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
               op =>
               {
                   op.LoginPath = "/Home/Login";
                   op.AccessDeniedPath = "/Home/Login";
                   op.ExpireTimeSpan = TimeSpan.FromMinutes(60);
               }
               );

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

// Routing Configuration for Details
app.MapControllerRoute(
    name: "details",
    pattern: "{table}/Details/{id}",
    defaults: new { controller = "Home", action = "Details" });

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
