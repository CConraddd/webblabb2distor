using System.ComponentModel.DataAnnotations;
using System;

namespace webblabb2distor.Models.Auctions
{
    public class CreateAuctionVm
    {
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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }
    }
}
