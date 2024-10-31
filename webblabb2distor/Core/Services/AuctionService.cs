using System.Collections.Generic;
using webblabb2distor.Core.Interfaces;
using System;

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
            var auction = _auctionPersistence.GetAuctionById(auctionId, username: null);
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
            var auction = _auctionPersistence.GetAuctionById(auctionId, username: null);
            if (auction == null)
            {
                throw new Exception("Auction not found");
            }
            return auction;
        }

        public IEnumerable<Auction> GetAuctionsByUserName(string username)
        {
            return _auctionPersistence.GetActiveAuctions()
                .Where(a => a.SellerUsername == username);
        }

        public IEnumerable<Auction> GetWonAuctions(string username)
        {
            return _auctionPersistence.GetActiveAuctions()
                .Where(a => a.Bids.Any(b => b.BidderUsername == username && b.Amount == a.Bids.Max(bid => bid.Amount))
                            && a.EndDateTime < DateTime.Now);
        }

        public void DeleteAuction(int auctionId)
        {
            _auctionPersistence.DeleteAuction(auctionId);
        }
    }
}
