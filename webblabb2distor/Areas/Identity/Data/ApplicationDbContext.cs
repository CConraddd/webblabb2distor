using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace webblabb2distor.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<webblabb2distorUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
