using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class AuctionService : IAuctionService
{
    public void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerId)
    {
        throw new NotImplementedException();
    }

    public void EditDescription(int auctionId, string newDescription)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Auction> GetAllActiveAuctions()
    {
        throw new NotImplementedException();
    }

    public Auction GetDetails(int auctionId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Auction> GetAuctionsByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Auction> GetWonAuctions(string userId)
    {
        throw new NotImplementedException();
    }
}