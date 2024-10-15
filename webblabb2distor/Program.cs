using Microsoft.EntityFrameworkCore;
using webblabb2distor.Persistence;
using Microsoft.AspNetCore.Identity;
using webblabb2distor.Areas.Identity.Data;
using webblabb2distor.Data;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuctionService, AuctionService>();

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ProjectDbConnection")));
builder.Services.AddDbContext<UserDbcontext>(options =>    
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));
builder.Services.AddDefaultIdentity<webblabb2distorUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<webblabb2distorContext>();

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

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();