namespace webblabb2distor.Core;
using System.Collections.Generic;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    public ICollection<Auction> Auctions { get; set; }
    public ICollection<Bid> Bids { get; set; }

    public User()
    {
        Auctions = new List<Auction>();
        Bids = new List<Bid>();
    }

    public void AddAuction(Auction auction)
    {
        Auctions.Add(auction);
    }

    public void AddBid(Bid bid)
    {
        Bids.Add(bid);
    }
}