using InRealTime.Data;
using InRealTime.Hubs;
using InRealTime.Models.ChatModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Database Connection
builder.Services.AddDbContext<AppDbContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));

// Add Cookie Auth
builder.Services.AddAuthentication()
    .AddCookie("default", opt =>
    {
        opt.Cookie.Name = "RealTimeCookie";
        opt.ExpireTimeSpan = TimeSpan.FromHours(8);
        opt.SlidingExpiration = true;
    });

// Service for Real Time Communiction
builder.Services.AddSignalR();

// Add Registry

builder.Services.AddSingleton<ChatRegistry>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/hubs/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
