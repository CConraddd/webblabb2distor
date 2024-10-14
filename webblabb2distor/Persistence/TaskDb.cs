using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// TaskDb.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webblabb2distor.Persistence;

public class TaskDb
{
    [Key]
    public int Id { get; set; } // Use 'Id' for consistency
    [Required]
    [MaxLength(64)]
    public string Description { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastUpdated { get; set; }

    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public ProjectDb Project { get; set; }
}
