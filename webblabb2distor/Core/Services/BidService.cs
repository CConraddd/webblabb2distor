using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class BidService : IBidService
{
    private readonly IAuctionService _auctionService;

    public BidService(IAuctionService auctionService)
    {
       _auctionService = auctionService;
    }

    public void PlaceBid(int auctionId, string bidderId, decimal bidAmount)
    {
        var auction = _auctionService.GetDetails(auctionId);
        if (auction is null || auction.EndDateTime < DateTime.Now)
        {
            throw new InvalidOperationException("Auction is either invalid, or end date is invalid.");
        }
        decimal highestBid;

        if (auction.SellerId == bidderId)
        {
            throw new InvalidOperationException("You cannot place bid on your own auction.");
        }
        if (auction.Bids.Any())
        {
            highestBid = auction.Bids.Max(b => b.Amount);
        }
        else
        {
            highestBid = auction.StartingPrice;
        }
        if (bidAmount <= highestBid)
        {
            throw new InvalidOperationException("Bid is not higher than the highest bid.");
        }
        auction.Bids.Add(new Bid(auction.Bids.Count+1, bidderId, bidAmount, DateTime.Now));
    }

    public IEnumerable<Bid> GetBidsForAuction(int auctionId)
    {
        var auction = _auctionService.GetDetails(auctionId);
        if (auction == null)
        {
            return new List<Bid>();
        }
        return auction.Bids.OrderByDescending(b => b.Amount);
    }

    public IEnumerable<Bid> GetBidsByUserId(string userId)
    {
        var auctions = _auctionService.GetAuctionsByUserId(userId);
        return auctions.SelectMany(a => a.Bids.Where(b => b.BidderId == userId));
    }
}