using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webblabb2distor.Persistence;

public class TaskDb
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(64)]
    public string Description { get; set; }
  
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdated { get; set; }

    [ForeignKey("projectId")]
    public ProjectDb Project { get; set; }
    public int ProjectId { get; set; }
    
}