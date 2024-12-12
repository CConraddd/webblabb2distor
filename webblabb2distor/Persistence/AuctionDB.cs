using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Persistence;

public class AuctionDB
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal StartingPrice { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDateTime{ get; set; }
    [Required]
    public string SellerUsername { get; set; }
    [Required]
    public List<BidsDb> BidsDbs { get; set; } = new List<BidsDb>();
}