namespace webblabb2distor.Core;

public class Bid
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public Auction Auction { get; set; }
    public string BidderUsername { get; set; } 
    public decimal Amount { get; set; }
    public DateTime BidTime { get; set; }

    public Bid(int id, int auctionId, string bidderUsername, decimal amount, DateTime bidTime)
    {
        Id = id;
        AuctionId = auctionId;
        BidderUsername = bidderUsername;
        Amount = amount;
        BidTime = bidTime;
    }
}