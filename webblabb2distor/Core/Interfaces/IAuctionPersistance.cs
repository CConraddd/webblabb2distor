namespace webblabb2distor.Core.Interfaces;

public interface IAuctionPersistance
{
    void AddAuction(Auction auction);
    
    Auction GetAuctionById(int auctionId);
    
    IEnumerable<Auction> GetActiveAuctions();
    
    void UpdateAuction(Auction auction);
    
    void DeleteAuction(int auctionId);
}