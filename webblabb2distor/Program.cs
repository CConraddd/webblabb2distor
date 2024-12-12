using Microsoft.EntityFrameworkCore;
using webblabb2distor.Persistence;
using Microsoft.AspNetCore.Identity;
using webblabb2distor.Areas.Identity.Data;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Core.Services;
using webblabb2distor.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Auction services
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionPersistence>();

builder.Services.AddAutoMapper(typeof(Program));

// Register ApplicationDbContext for Identity (Users)
builder.Services.AddDbContext<webblabb2distorContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));

// Register Identity with ApplicationDbContext
builder.Services.AddDefaultIdentity<webblabb2distorUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<webblabb2distorContext>();

// Register AuctionDbContext for Auction data
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auction}/{action=Index}/{id?}");

app.Run();