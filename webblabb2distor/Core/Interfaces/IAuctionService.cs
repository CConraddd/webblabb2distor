namespace webblabb2distor.Core.Interfaces;

public interface IAuctionService
{
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerUsername);
    void EditDescription(int auctionId, string newDescription);
    List<Auction> GetAllActiveAuctions();
    Auction GetDetails(int auctionId);
    IEnumerable<Auction> GetAuctionsByUserName(string username);
    IEnumerable<Auction> GetWonAuctions(string username);
    void DeleteAuction(int auctionId);
}