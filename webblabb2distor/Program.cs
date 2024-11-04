using Microsoft.EntityFrameworkCore;
using webblabb2distor.Persistence;
using Microsoft.AspNetCore.Identity;
using webblabb2distor.Areas.Identity.Data;
using webblabb2distor.Core.Interfaces;
using webblabb2distor.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Auction services
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<IAuctionPersistence, AuctionPersistence>();

builder.Services.AddAutoMapper(typeof(Program));

// Register ApplicationDbContext for Identity (Users)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));

// Register Identity with ApplicationDbContext
builder.Services.AddDefaultIdentity<webblabb2distorUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>(); // Use ApplicationDbContext for Identity


builder.Services.AddDbContext<UserDbcontext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection"))); 


// Register AuctionDbContext for Auction data
builder.Services.AddDbContext<AuctionDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ProjectDbConnection")));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();