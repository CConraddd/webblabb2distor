using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Persistence;

public class ProjectDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public String Title { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDate { get; set; }
    
    public List<TaskDb> TaskDbs { get; set; } = new List<TaskDb>();
}