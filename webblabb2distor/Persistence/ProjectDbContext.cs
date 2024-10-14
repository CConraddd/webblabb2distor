using Microsoft.EntityFrameworkCore;

namespace webblabb2distor.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
    public DbSet<TaskDb> TasksDbs { get; set; }
    public DbSet<ProjectDb> ProjectDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ProjectDb pdb = new ProjectDb
        {
            Id = -1,
            Title = "testlearn Aspnet",
            CreatedDate = DateTime.Now,
            TaskDbs = new List<TaskDb>()
        };
        modelBuilder.Entity<TaskDb>().HasData(pdb);

        TaskDb tbd2 = new TaskDb()
        {
            id = -1,
            Description = "test test la la",
            LastUpdated = DateTime.Now,
            ProjectId = -1,
        };
        
        modelBuilder.Entity<TaskDb>().HasData(tbd2);
        

    }
    
}