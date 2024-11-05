using webblabb2distor.Core;

namespace webblabb2distor.Persistence;

using Microsoft.EntityFrameworkCore;


public class UserDbcontext : DbContext
{

    
    //creates the userdatabase from the userVM.
    public UserDbcontext(DbContextOptions<UserDbcontext> options) : base(options){}
   
    public DbSet<UserDb> UserDbs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDb>().HasData(
            new UserDb
            {
                id = 1,
                Password = "Orvar123!",
                Username = "Bertil@kth.se",
            },
            new UserDb
            {
                id = 2,
                Password = "123Gaming",
                Username = "Kalle@kth.se",
            },
            new UserDb
            {
                id = 3,
                Password = "123Gaming",
                Username = "bertil@kth.se",
            }
            
        );
        
    }
    }
    
    