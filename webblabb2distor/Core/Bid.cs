namespace webblabb2distor.Core;

public class Bid
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public Auction Auction { get; set; }
    public string Biddername { get; set; } 
    public decimal Bidamount { get; set; }
    public DateTime Bidtime { get; set; }

    public Bid(int id, int auctionId, string bidderUsername, decimal amount, DateTime bidTime)
    {
        Id = id;
        AuctionId = auctionId;
        Biddername = bidderUsername;
        Bidamount = amount;
        Bidtime = bidTime;
    }
    
    public Bid() {}
}