namespace webblabb2distor.Core;

public class Bid
{
    public int Id { get; set; }
    public int AuctionId { get; set; }
    public string BidderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime BidTime { get; set; }
}