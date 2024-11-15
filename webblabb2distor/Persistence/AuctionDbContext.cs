using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace webblabb2distor.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    public DbSet<BidsDb> BidsDbs { get; set; }
    public DbSet<AuctionDB> AuctionDbs { get; set; }
    //public DbSet<UserDb> UserDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding `AuctionDB` data
        modelBuilder.Entity<AuctionDB>().HasData(
            new AuctionDB
            {
                Id = -1,
                Name = "ragnar",
                Description = "Auction description",
                EndDateTime = DateTime.Today.AddDays(7),
                StartingPrice = 45,
                SellerUsername = "Seller"
            },
            new AuctionDB
            {
                Id = -2,
                Name = "bartil",
                Description = "Old ferrari",
                EndDateTime = DateTime.Today.AddDays(10),
                StartingPrice = 25000,
                SellerUsername = "bertil"
            }
        );

        // Seeding `BidsDb` data with references to existing `AuctionDB` entries
        modelBuilder.Entity<BidsDb>().HasData(
            new BidsDb
            {
                Id = -1,
                AuctionId = -1,
                Bidamount = 35,
                Biddername = "byuer",
                Bidtime = DateTime.Today.AddDays(5)
            },
            new BidsDb
            {
                Id = -2,
                AuctionId = -2,
                Bidamount = 20000,
                Biddername = "Kalle",
                Bidtime = DateTime.Today.AddDays(8)
            },
            new BidsDb
                {
                    Id = 1,
                    AuctionId = -1,
                    Bidamount = 50,
                    Biddername = "ferrari@gmail.com",
                    Bidtime = DateTime.Now
                }
        );
/*
        modelBuilder.Entity<UserDb>().HasData(
            new UserDb
            {
                id = 1,
                Password = "Orvar123!",
                    Username = "Bertil@kth.se", 
            },
            new UserDb
            {
                id = 2,
                Password = "123Gaming",
                Username = "Kalle@kth.se",
            }
        );
        */
    }
}