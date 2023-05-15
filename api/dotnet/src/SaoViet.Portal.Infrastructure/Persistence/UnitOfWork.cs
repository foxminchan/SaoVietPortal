using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context) => _context = context;

    public void Commit() => _context.SaveChanges();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}