using Microsoft.EntityFrameworkCore;


namespace webblabb2distor.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

    public DbSet<BidsDb> BidsDbs { get; set; }
    public DbSet<AuctionDB> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding initial data
        modelBuilder.Entity<AuctionDB>().HasData(new AuctionDB
        {
         Id  = 1,
         description = "Auction description",
         Enddate = DateTime.Today,
         BidsDbs = new List<BidsDb>(),
         price = 45,
         Sellername = "Seller",
        });

        modelBuilder.Entity<BidsDb>().HasData(new BidsDb
        {
         Id = 2,
         Bidamount = 35,
         Biddername = "byuer",
         Bidtime = DateTime.Today
        });
    }
}