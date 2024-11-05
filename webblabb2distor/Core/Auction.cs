namespace webblabb2distor.Core;

public class Auction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal StartingPrice { get; set; }
    public DateTime EndDateTime { get; set; }
    public string SellerUsername { get; set; } 
    public List<Bid> Bids { get; set; }
    public Auction()
    {
        Bids = new List<Bid>();
    }
    public Auction(int id, string name, string description, decimal startingPrice, DateTime endDateTime, string sellerUsername)
    {
        Id = id;
        Name = name;
        Description = description;
        StartingPrice = startingPrice;
        EndDateTime = endDateTime;
        SellerUsername = sellerUsername;
        Bids = new List<Bid>();
    }
}