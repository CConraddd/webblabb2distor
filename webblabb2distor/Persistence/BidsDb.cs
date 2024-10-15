using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// TaskDb.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webblabb2distor.Persistence;

public class BidsDb
{
    [Key]
    public int Id { get; set; } // Use 'Id' for consistency
 
    [Required]
    public decimal Bidamount { get; set; }
    [Required]
    public string Biddername { get; set; }
    
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Bidtime { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public AuctionDB Auction { get; set; }
}
