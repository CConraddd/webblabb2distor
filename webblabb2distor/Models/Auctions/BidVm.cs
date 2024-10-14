using System.ComponentModel.DataAnnotations;
using webblabb2distor.Core;

namespace webblabb2distor.Models.Auctions;

public class BidVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public decimal BidAmount { get; set; }
    [Display(Name = "Bidder")]
    public string BidderName { get; set; }
    [Display(Name = "Bid Time")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime BidTime { get; set; }

    public static BidVm FromBid(Bid bid)
    {
        return new BidVm
        {
            Id = bid.Id,
            BidAmount = bid.Amount,
            BidderName = bid.BidderId.Name,
            BidTime = bid.BidTime
        };
    }
}