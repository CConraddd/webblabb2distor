using webblabb2distor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using webblabb2distor.Persistence;
using Microsoft.EntityFrameworkCore;
using webblabb2distor.Core;

namespace webblabb2distor.Persistence;

public class AuctionPersistence : IAuctionPersistence
{
    
    private readonly AuctionDbContext _dbContext;

    public AuctionPersistence(AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName)
    {
        var auction = new AuctionDB
        {
            name = name,
            description = description,
            price = startingPrice,
            Enddate = endDate,
            Sellername = userName
        };
        _dbContext.AuctionDbs.Add(auction);
        _dbContext.SaveChanges();
    }
    
    public AuctionDB GetAuctionById(int auctionId, string username)
    {
        return _dbContext.AuctionDbs.Include(a => a.BidsDbs).FirstOrDefault(a => a.Id == auctionId && a.Sellername == username);
    }

    public List<Auction> GetActiveAuctions()
    {
        return _dbContext.AuctionDbs
            .Where(a => a.Enddate > DateTime.Now)
            .ToList();
    }

    public void UpdateAuction(AuctionDB auction)
    {
        _dbContext.AuctionDbs.Update(auction);
        _dbContext.SaveChanges();
    }

    public void DeleteAuction(int auctionId)
    {
        var auction = _dbContext.AuctionDbs.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            _dbContext.AuctionDbs.Remove(auction);
            _dbContext.SaveChanges();
        }
    }
}
