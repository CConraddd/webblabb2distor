using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class BidService : IBidService
{
    private readonly IAuctionService _auctionService;
    private readonly UserService _userService;

    public BidService(IAuctionService auctionService, UserService userService)
    {
       _auctionService = auctionService;
       _userService = userService;
    }

    public void PlaceBid(int auctionId, int bidderId, decimal bidAmount)
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
        var bidder = _userService.GetUserById(bidderId);
        if (bidder == null)
        {
            throw new InvalidOperationException("Bidder not found.");
        }
        auction.Bids.Add(new Bid(auction.Bids.Count+1, bidderId, bidAmount, DateTime.Now, bidder));
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

    public IEnumerable<Bid> GetBidsByUserId(int userId)
    {
        var auctions = _auctionService.GetAuctionsByUserId(userId);
        return auctions.SelectMany(a => a.Bids.Where(b => b.BidderId == userId));
    }
}