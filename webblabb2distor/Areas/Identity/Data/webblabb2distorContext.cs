using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webblabb2distor.Areas.Identity.Data;

namespace webblabb2distor.Data;

public class webblabb2distorContext : IdentityDbContext<webblabb2distorUser>
{
    public webblabb2distorContext(DbContextOptions<webblabb2distorContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
