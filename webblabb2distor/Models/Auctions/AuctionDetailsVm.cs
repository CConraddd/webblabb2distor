using System.ComponentModel.DataAnnotations;
using webblabb2distor.Core;

namespace webblabb2distor.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Display(Name = "Start Price")]
    public decimal StartingPrice { get; set; }
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDateTime { get; set; }
    public string SellerName { get; set; }
    public List<BidVm> Bids { get; set; } = new();

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var auctionDetailsVm = new AuctionDetailsVm
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            StartingPrice = auction.StartingPrice,
            EndDateTime = auction.EndDateTime,
            SellerName = auction.SellerUsername
        };
        foreach (var bid in auction.Bids)
        {
            auctionDetailsVm.Bids.Add(BidVm.FromBid(bid));
        }

        return auctionDetailsVm;
    }
}