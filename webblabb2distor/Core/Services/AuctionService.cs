using System.Collections.Generic;
using webblabb2distor.Core.Interfaces;
using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace webblabb2distor.Core.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionPersistence _auctionPersistence;

        public AuctionService(IAuctionPersistence auctionPersistence)
        {
            _auctionPersistence = auctionPersistence;
        }

        public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerUsername)
        {
            _auctionPersistence.CreateAuction(name, description, startingPrice, endDate, sellerUsername);
        }

        public void EditDescription(int auctionId, string newDescription)
        {
            var auction = _auctionPersistence.GetAuctionById(auctionId);
            if (auction != null)
            {
                auction.Description = newDescription;
                _auctionPersistence.UpdateAuction(auction);
            }
        }

        public List<Auction> GetAllActiveAuctions()
        {
            return _auctionPersistence.GetActiveAuctions();
        }

        public Auction GetDetails(int auctionId)
        {
            Console.WriteLine($"Fetching Auction with ID: {auctionId}");
            var auction = _auctionPersistence.GetAuctionById(auctionId);

            if (auction == null)
            {
                Console.WriteLine($"Auction with ID {auctionId} not found.");
                throw new Exception("Auction not found");
            }

            Console.WriteLine($"Auction found: {auction.Name} with {auction.Bids.Count} bids.");
            return auction;
        }

        public IEnumerable<Auction> GetAuctionsByUserName(string username)
        {
            return _auctionPersistence.GetActiveAuctions()
                .Where(a => a.SellerUsername == username);
        }
        
        public List<Auction> GetAllAuctions()
        {
            var auctions = _auctionPersistence.GetAllAuctions();
            foreach (var auction in auctions)
            {
                Console.WriteLine($"Auction ID: {auction.Id}, Name: {auction.Name}, Bids Count: {auction.Bids.Count}");
            }
            return auctions;
        }
        
        public IEnumerable<Auction> GetWonAuctions(string username)
        {
            var allAuctions = _auctionPersistence.GetAllAuctions();
            return allAuctions
                .Where(a => a.Bids.Any(b => b.Biddername == username && b.Bidamount == a.Bids.Max(bid => bid.Bidamount))
                            && a.EndDateTime < DateTime.Now);
        }

        public void DeleteAuction(int auctionId)
        {
            _auctionPersistence.DeleteAuction(auctionId);
        }
        
        public void AddBidToAuction(int auctionId, Bid bid)
        {
            var auction = _auctionPersistence.GetAuctionById(auctionId);
            if (auction == null)
            {
                throw new DataException("Auction not found.");
            }

            auction.Bids.Add(bid);

            try
            {
                _auctionPersistence.UpdateAuction(auction);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error while adding bid: " + ex.Message);
                throw new DataException("Could not save the bid to the database.", ex);
            }
        }


    }
}
