using webblabb2distor.Persistence;
using System.Collections.Generic;
using System;

namespace webblabb2distor.Core.Interfaces;

public interface IAuctionPersistence
{
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName);
    Auction GetAuctionById(int auctionId);
    List<Auction> GetActiveAuctions();
    void UpdateAuction(Auction auction);
    void DeleteAuction(int auctionId);
    List<Auction> GetAllAuctions();
}