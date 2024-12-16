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
            if (startingPrice <= 0)
            {
                throw new ArgumentException("Starting price must be greater than zero");
            }

            if (endDate < DateTime.Now)
            {
                throw new ArgumentException("End date must be in the future");
            }
            _auctionPersistence.CreateAuction(name, description, startingPrice, endDate, sellerUsername);
        }

        public void EditDescription(int auctionId, string newDescription, string currentUser)
        {
            var auction = _auctionPersistence.GetAuctionById(auctionId);
            if (auction == null)
            {
                throw new DataException("Auction not found");
            }

            if (auction.SellerUsername != currentUser)
            {
                throw new UnauthorizedAccessException("You are not authorized to edit this auction");
            }

            auction.Description = newDescription;
            try
            {
                _auctionPersistence.UpdateAuction(auction);
            }
            catch
            {
                throw new DataException("Could not update auction");
            }
        }

        public List<Auction> GetAllActiveAuctions()
        {
            return _auctionPersistence.GetActiveAuctions();
        }

        public Auction GetDetails(int auctionId)
        {
            var auction = _auctionPersistence.GetAuctionById(auctionId);

            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            auction.Bids = auction.Bids.OrderByDescending(b => b.Bidamount).ToList();
            return auction;
        }

        
        //returns auctionbyusername
        public IEnumerable<Auction> GetAuctionsByUserName(string username)
        {
            return _auctionPersistence.GetActiveAuctions()
                .Where(a => a.SellerUsername == username);
        }
        //returns all auctions
        public List<Auction> GetAllAuctions()
        {
            var auctions = _auctionPersistence.GetAllAuctions();
            return auctions;
        }
        //returns all won auctions
        public IEnumerable<Auction> GetWonAuctions(string username)
        {
            var allAuctions = _auctionPersistence.GetAllAuctions();
           return allAuctions.Where(a => a.EndDateTime <= DateTime.Now &&
                                  a.Bids.Any(b => b.Biddername == username && 
                                                  b.Bidamount == a.Bids.Max(bid => bid.Bidamount)));
        }
        //delets a selected auction
        public void DeleteAuction(int auctionId, string currentUser)
        {
            var auction = _auctionPersistence.GetAuctionById(auctionId);
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }

            if (auction.SellerUsername != currentUser)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this auction.");
            }

            _auctionPersistence.DeleteAuction(auctionId);
        }
        //adds bid to auction
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
