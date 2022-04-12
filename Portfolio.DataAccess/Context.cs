using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entity;

namespace Portfolio.DataAccess;

public class Context : IdentityDbContext<User>
{
    public Context(DbContextOptions<Context> options)
        :base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }
}