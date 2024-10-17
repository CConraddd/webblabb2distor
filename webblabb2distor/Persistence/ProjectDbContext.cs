using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace webblabb2distor.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

    public DbSet<BidsDb> BidsDbs { get; set; }
    public DbSet<AuctionDB> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding `AuctionDB` data
        modelBuilder.Entity<AuctionDB>().HasData(
            new AuctionDB
            {
                Id = 1,
                name = "ragnar",
                description = "Auction description",
                Enddate = DateTime.Today,
                price = 45,
                Sellername = "Seller"
            },
            new AuctionDB
            {
                Id = 2,
                name = "bartil",
                description = "Old ferrari",
                Enddate = DateTime.Today,
                price = 25000,
                Sellername = "bertil"
            }
        );

        // Seeding `BidsDb` data with references to existing `AuctionDB` entries
        modelBuilder.Entity<BidsDb>().HasData(
            new BidsDb
            {
                Id = 1,
                AuctionId = 1,  // Refers to the first AuctionDB entry
                Bidamount = 35,
                Biddername = "byuer",
                Bidtime = DateTime.Today
            },
            new BidsDb
            {
                Id = 2,
                AuctionId = 2,  // Refers to the second AuctionDB entry
                Bidamount = 20000,
                Biddername = "Kalle",
                Bidtime = DateTime.Today
            }
        );
    }
}