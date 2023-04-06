using Portal.Domain.Interfaces;
using Portal.Domain.Interfaces.Common;

namespace Portal.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IStudentRepository studentRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        studentRepository = new StudentRepository(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}