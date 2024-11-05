using System;
using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Models.Auctions
{
    public class CreateBidVm
    {
        [Required(ErrorMessage = "Bid amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero")]
        public decimal BidAmount { get; set; }
        
        public int AuctionId { get; set; }
    }
}