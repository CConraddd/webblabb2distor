namespace webblabb2distor.Core.Interfaces;

public interface IAuctionPersistance
{
    
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string userName);

    void AddAuction(Auction auction);
    
    Auction GetAuctionById(int auctionId, String username);
    
    IEnumerable<Auction> GetActiveAuctions();
    
    void UpdateAuction(Auction auction);
    
    void DeleteAuction(int auctionId);
}