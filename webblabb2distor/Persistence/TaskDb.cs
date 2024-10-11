using System.ComponentModel.DataAnnotations;

namespace webblabb2distor.Persistence;

public class TaskDb
{
    [Key]
    public int id { get; set; }
    [Required]
    [MaxLength(64)]
    public string Title { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }
    
    public List<TaskDb> TaskDbs { get; set; } = new List<TaskDb>();
}