using OneKey.Entity;

namespace OneKey.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public AppDbContext Context { get => _context; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}

public interface IUnitOfWork
{
    public AppDbContext Context { get; }
    Task SaveChangesAsync();
}

