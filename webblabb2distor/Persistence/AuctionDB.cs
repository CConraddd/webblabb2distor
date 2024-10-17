using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Persistence;

public class AuctionDB
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public String name { get; set; }
    [Required]
    public String description { get; set; }
    [Required]
    public decimal price { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Enddate { get; set; }
    [Required]
    public string Sellername { get; set; }
    [Required]
    public List<BidsDb> BidsDbs { get; set; } = new List<BidsDb>();
}