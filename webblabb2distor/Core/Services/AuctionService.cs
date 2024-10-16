using System.Data;
using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class AuctionService : IAuctionService
{
    private readonly List<Auction> _auctions = new List<Auction>();
    
    public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerUsername)
    {
        _auctions.Add(new Auction(_auctions.Count + 1, name, description, startingPrice, endDate, sellerUsername));
    }

    public void EditDescription(int auctionId, string newDescription)
    {
        var auction = _auctions.Find(auction => auction.Id == auctionId);
        if (auction != null)
        {
            auction.Description = newDescription;
        }
    }

    public IEnumerable<Auction> GetAllActiveAuctions()
    {
        return _auctions.Where(auction => auction.EndDateTime > DateTime.Now);
    }

    public Auction GetDetails(int auctionId)
    {
        var auction = _auctions.Find(a => a.Id == auctionId);

        if (auction == null)
        {
            throw new DataException("Auction not found");
        }

        return auction;
    }

    public IEnumerable<Auction> GetAuctionsByUserName(string username)
    {
        return _auctions.Where(a => a.SellerUsername == username);
    }

    public IEnumerable<Auction> GetWonAuctions(string username)
    {
        return _auctions.Where(a => a.Bids.Any(b => b.BidderUsername == username && b.Amount == a.Bids.Max(bid => bid.Amount))
                                    && a.EndDateTime < DateTime.Now);
    }
}