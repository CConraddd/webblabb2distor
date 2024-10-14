namespace webblabb2distor.Core;

public class Auction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime EndDateTime { get; set; }
    public string SellerId { get; set; }
    public List<Bid> Bids { get; set; }

    public Auction(int id, string name, string description, decimal startingPrice, DateTime endDateTime, string sellerId)
    {
        Id = id;
        Name = name;
        Description = description;
        StartingPrice = startingPrice;
        EndDateTime = endDateTime;
        SellerId = sellerId;
        Bids = new List<Bid>(); 
    }
}