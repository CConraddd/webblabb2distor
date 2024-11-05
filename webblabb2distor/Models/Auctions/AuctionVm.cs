using System;
using System.ComponentModel.DataAnnotations;
using webblabb2distor.Core;

namespace webblabb2distor.Models.Auctions;

public class AuctionVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Starting Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Starting Price must be a positive number")]
    public decimal StartingPrice { get; set; }

    [Required(ErrorMessage = "End Date is required")]
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime EndDateTime { get; set; }

    [ScaffoldColumn(false)]
    public string SellerUsername { get; set; }

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm
        {
            Id = auction.Id,
            Name = auction.Name,
            Description = auction.Description,
            StartingPrice = auction.StartingPrice,
            EndDateTime = auction.EndDateTime,
            SellerUsername = auction.SellerUsername
        };
    }

    /*
     public Auction ToAuction()
    {
        return new Auction
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description,
            StartingPrice = this.StartingPrice,
            EndDateTime = this.EndDateTime,
            SellerUsername = this.SellerUsername
        };
    }*/
}