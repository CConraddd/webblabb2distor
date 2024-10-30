using webblabb2distor.Persistence;
using System.Collections.Generic;
using System;

namespace webblabb2distor.Core.Interfaces;

public interface IAuctionPersistence
{
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName);
    AuctionDB GetAuctionById(int auctionId, string username);
    List<Auction> GetActiveAuctions();
    void UpdateAuction(AuctionDB auction);
    void DeleteAuction(int auctionId);
}