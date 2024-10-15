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
           
        });

        modelBuilder.Entity<BidsDb>().HasData(new BidsDb
        {
         
        });
    }
}