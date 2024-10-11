using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class BidService : IBidService
{
    public void PlaceBid(int auctionId, string bidderId, decimal bidAmount)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bid> GetBidsForAuction(int auctionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bid> GetBidsByUserId(int userId)
    {
        throw new NotImplementedException();
    }
}