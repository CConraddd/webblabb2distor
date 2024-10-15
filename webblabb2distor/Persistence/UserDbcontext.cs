using webblabb2distor.Core;

namespace webblabb2distor.Persistence;

using Microsoft.EntityFrameworkCore;


public class UserDbcontext : DbContext
{

    public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options){}
    public DbSet <UserDb> UserDbs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDb>().HasData(new UserDb
            {
                id =1,
                Password = "123456",
                Username = "bertil",
            });
        
        modelBuilder.Entity<UserDb>().HasData(new UserDb
        {
            id =2,
            Password = "2222",
            Username = "ragnar",
        });
        
        modelBuilder.Entity<UserDb>().HasData(new UserDb
        {
            id =3,
            Password = "4444",
            Username = "hagnar",
        });







    }
    }