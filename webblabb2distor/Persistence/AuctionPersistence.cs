using webblabb2distor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using webblabb2distor.Core;

namespace webblabb2distor.Persistence;

public class AuctionPersistence : IAuctionPersistance
{
     
    
    // In-memory storage for auctions
    private readonly List<Auction> _auctions = new();
    private int _nextId = 1; // Simple ID generator

    public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName)
    {
        var auction = new Auction
        {
            Id = _nextId++,
            Name = name,
            Description = description,
            StartingPrice = startingPrice,
            EndDateTime = endDate,
            SellerUsername = userName
        };

        _auctions.Add(auction);
    }
    
    public void AddAuction(Auction auction)
    {
        auction.Id = _nextId++;
        _auctions.Add(auction);
    }

    public Auction GetAuctionById(int auctionId, string username)
    {
        return _auctions.FirstOrDefault(a => a.Id == auctionId && a.SellerUsername == username);
    }

    public IEnumerable<Auction> GetActiveAuctions()
    {
        return _auctions.Where(a => a.EndDateTime > DateTime.Now).ToList();
    }

    public void UpdateAuction(Auction auction)
    {
        var existingAuction = _auctions.FirstOrDefault(a => a.Id == auction.Id);
        if (existingAuction != null)
        {
            existingAuction.Name = auction.Name;
            existingAuction.Description = auction.Description;
            existingAuction.StartingPrice = auction.StartingPrice;
            existingAuction.EndDateTime = auction.EndDateTime;
            existingAuction.SellerUsername = auction.SellerUsername;
        }
    }

    public void DeleteAuction(int auctionId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            _auctions.Remove(auction);
        }
    }
}
