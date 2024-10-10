namespace webblabb2distor.Core.Interfaces;

public interface IBidService
{
    void PlaceBid(int auctionId, string bidderId, decimal bidAmount);
    IEnumerable<Bid> GetBidsForAuction(int auctionId);
    IEnumerable<Bid> GetBidsByUserId(int userId);
}