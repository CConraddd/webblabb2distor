using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Models.Auctions;

public class CreateAuctionVm
{
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Starting Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Starting Price must be a positive number")]
    public decimal StartingPrice { get; set; }

    [Required(ErrorMessage = "End Date is required")]
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    public DateTime EndDateTime { get; set; }
}