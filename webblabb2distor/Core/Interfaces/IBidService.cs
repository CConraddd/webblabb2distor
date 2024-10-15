namespace webblabb2distor.Core.Interfaces;

public interface IBidService
{
    void PlaceBid(int auctionId, int bidderId, decimal bidAmount);
    IEnumerable<Bid> GetBidsForAuction(int auctionId);
    IEnumerable<Bid> GetBidsByUserId(int userId);
}