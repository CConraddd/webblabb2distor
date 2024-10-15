using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace webblabb2distor.Persistence;

public class UserDb
{
    [Key]
    public int id { get; set; }
    
    [Required]
    public String Username { get; set; } = null!;
    
    [Required]
    public String Password { get; set; } = null!;
    
}