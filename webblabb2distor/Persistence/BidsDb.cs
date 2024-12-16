using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// TaskDb.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webblabb2distor.Persistence;




public class BidsDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    public decimal Bidamount { get; set; }
    [Required]
    public string Biddername { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Bidtime { get; set; }

    [ForeignKey("AuctionId")]
    public int AuctionId { get; set; }
    [Required]
    public AuctionDB Auction { get; set; }
}
