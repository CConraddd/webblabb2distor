namespace webblabb2distor.Core;

public class Bid
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public int BidderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BidTime { get; set; }
    public User Bidder { get; set; }

    public Bid(int auctionId, int bidderId, decimal amount, DateTime bidTime, User bidder)
    {
        AuctionId = auctionId;
        BidderId = bidderId;
        Amount = amount;
        BidTime = bidTime;
        Bidder = bidder;
    }
}