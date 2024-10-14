using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class AuctionService : IAuctionService
{
    private readonly List<Auction> _auctions = new List<Auction>();
    public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerId)
    { 
        _auctions.Add(new Auction(_auctions.Count + 1, name, description, startingPrice, endDate, sellerId));
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
        return _auctions.Where(auction => auction.EndDateTime <= DateTime.Now);
    }

    public Auction GetDetails(int auctionId)
    {
        return _auctions.Find(auction => auction.Id == auctionId);
    }

    public IEnumerable<Auction> GetAuctionsByUserId(string userId)
    {
        return _auctions.Where(a => a.SellerId == userId);
    }

    public IEnumerable<Auction> GetWonAuctions(string userId)
    {
        return _auctions.Where(a => a.Bids.Any(b => b.BidderId == userId && b.Amount == a.Bids.Max(bid => bid.Amount))&& a.EndDateTime < DateTime.Now);
    }
}