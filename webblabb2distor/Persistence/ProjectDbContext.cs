using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace webblabb2distor.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

    public DbSet<TaskDb> TasksDbs { get; set; }
    public DbSet<ProjectDb> ProjectDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding initial data
        modelBuilder.Entity<ProjectDb>().HasData(new ProjectDb
        {
            Id = 4,
            Title = "testlearn Aspnet",
            CreatedDate = DateTime.Now,
        });

        modelBuilder.Entity<TaskDb>().HasData(new TaskDb
        {
            Id = 2,
            Description = "test test la la",
            LastUpdated = DateTime.Now,
            ProjectId = 3,
        });
    }
}