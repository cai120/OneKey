using OneKey.Shared.Utilities;
using OneKey.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OneKey.Entity.Models;

namespace OneKey.Entity;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach(var added in ChangeTracker.Entries<IBaseEntity>().Where(a => a.State == EntityState.Added))
        {
            if (!string.IsNullOrEmpty(added.Entity.Reference))
                continue;

            added.Entity.Reference = Guid.NewGuid().ToString();
        }

        return base.SaveChangesAsync(cancellationToken);    
    }
}