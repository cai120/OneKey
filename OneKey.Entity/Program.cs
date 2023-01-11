using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OneKey.Entity;

public static class Program
{
    public static void Main(string[] args)
    {
    }
}
    
    
public class DbContextMigrationFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        builder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=OneKeyDatabase;trusted_connection=yes;MultipleActiveResultSets=True");
        return new AppDbContext(builder.Options);
    }
}
