using webblabb2distor.Core.Interfaces;

namespace webblabb2distor.Core.Services;

public class BidService : IBidService
{
    private readonly IAuctionService _auctionService;

    public BidService(IAuctionService auctionService)
    {
        _auctionService = auctionService;
    }

    public void PlaceBid(int auctionId, string bidderUsername, decimal bidAmount)
    {
        var auction = _auctionService.GetDetails(auctionId);
        if (auction is null || auction.EndDateTime < DateTime.Now)
        {
            throw new InvalidOperationException("Auction is either invalid, or end date is invalid.");
        }

        if (auction.SellerUsername == bidderUsername)
        {
            throw new InvalidOperationException("You cannot place bid on your own auction.");
        }

        decimal highestBid = auction.Bids.Any() ? auction.Bids.Max(b => b.Bidamount) : auction.StartingPrice;
        if (bidAmount <= highestBid)
        {
            throw new InvalidOperationException("Bid is not higher than the highest bid.");
        }

        var newBid = new Bid { Biddername = bidderUsername, Bidamount = bidAmount, Bidtime = DateTime.Now };
        _auctionService.AddBidToAuction(auctionId, newBid);
    }

    public IEnumerable<Bid> GetBidsForAuction(int auctionId)
    {
        var auction = _auctionService.GetDetails(auctionId);
        if (auction == null)
        {
            return new List<Bid>();
        }
        return auction.Bids.OrderByDescending(b => b.Bidamount);
    }

    public IEnumerable<Bid> GetBidsByUsername(string username)
    {
        var auctions = _auctionService.GetAllAuctions()
            .Where(a => a.EndDateTime > DateTime.Now) // Endast pågående auktioner
            .ToList();

        foreach (var auction in auctions)
        {
            Console.WriteLine($"Auction ID: {auction.Id}, Name: {auction.Name}, Bids Count: {auction.Bids.Count}");
        }

        var userBids = auctions
            .SelectMany(a => a.Bids)
            .Where(b => b.Biddername == username)
            .GroupBy(b => b.AuctionId) // Grupp efter auktion
            .Select(g => g.OrderByDescending(b => b.Bidtime).FirstOrDefault()) // Ta senaste budet per auktion
            .ToList();

        foreach (var bid in userBids)
        {
            Console.WriteLine($"Latest Bid - Bid ID: {bid.Id}, Auction ID: {bid.AuctionId}, Amount: {bid.Bidamount}");
        }

        return userBids;
    }




}