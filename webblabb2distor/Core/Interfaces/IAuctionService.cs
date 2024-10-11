namespace webblabb2distor.Core.Interfaces;

public interface IAuctionService
{
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerId);
    void EditDescription(int auctionId, string newDescription);
    IEnumerable<Auction> GetAllActiveAuctions();
    Auction GetDetails(int auctionId);
    IEnumerable<Auction> GetAuctionsByUserId(string userId);
    IEnumerable<Auction> GetWonAuctions(string userId);
}