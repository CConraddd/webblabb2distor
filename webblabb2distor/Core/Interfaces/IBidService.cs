namespace webblabb2distor.Core.Interfaces;

public interface IBidService
{
    void PlaceBid(int auctionId, string bidderUsername, decimal bidAmount);
    IEnumerable<Bid> GetBidsForAuction(int auctionId);
    IEnumerable<Bid> GetBidsByUsername(string username);
    
}