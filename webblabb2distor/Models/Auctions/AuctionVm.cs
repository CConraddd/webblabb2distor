using System.ComponentModel.DataAnnotations;
using webblabb2distor.Core;

namespace webblabb2distor.Models.Auctions;

public class AuctionVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    [Display(Name = "Start Time")]
    public decimal StartingPrice { get; set; }
    
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDateTime { get; set; }

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            StartingPrice = auction.StartingPrice,
            EndDateTime = auction.EndDateTime
        };
    }
}