namespace webblabb2distor.Core.Interfaces;

public interface IAuctionService
{
    void CreateAuction(string name, string description, decimal startingPrice, DateTime endDate, string sellerId);
    void EditDescription(int auctionId, string newDescription);
    IEnumerable<Auction> GetAllActiveAuctions();
    Auction GetDetails(int auctionId);
    IEnumerable<Auction> GetAuctionsByUserId(string userId);
    IEnumerable<Auction> GetWonAuctions(string userId);
}

/*
 *skapa en auktion,
   editera beskrivningen av en auktion (men ingen annan information om auktionen),
   lista alla pågående auktioner (som ej passerat tid då auktionen går ut) - sorterade efter slutdatum,
   se detaljer, inklusive lagda bud, för en pågående auktion - buden ska vara sorterade så att det högsta ligger först,
   lägga bud på en pågående auktion, som inte är ens egen (budet måste vara högre än tidigare bud/utgångspris),
   lista alla auktioner där hen lagt bud och som är pågående,
   lista alla avslutade auktioner som hen vunnit. 
   
    public int Id { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public decimal StartingPrice { get; set; }
      public DateTime EndDate { get; set; }
      public string SellerId { get; set; }
      public List<Bid> Bids { get; set; }
 */