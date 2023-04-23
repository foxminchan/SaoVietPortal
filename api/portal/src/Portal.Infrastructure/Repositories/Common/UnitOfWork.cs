using Portal.Domain.Interfaces;
using Portal.Domain.Interfaces.Common;

namespace Portal.Infrastructure.Repositories.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IStudentRepository StudentRepository { get; private set; }

    public IBranchRepository BranchRepository { get; private set; }

    public IClassRepository ClassRepository { get; private set; }

    public ICourseRepository CourseRepository { get; private set; }

    public IPaymentMethodRepository PaymentMethodRepository { get; private set; }

    public ICourseRegistrationRepository CourseRegistrationRepository { get; private set; }

    public IPositionRepository PositionRepository { get; private set; }

    public IReceiptsExpensesRepository ReceiptsExpensesRepository { get; private set; }

    public IStaffRepository StaffRepository { get; private set; }

    public IStudentProgressRepository StudentProgressRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        StudentRepository = new StudentRepository(_context);
        BranchRepository = new BranchRepository(_context);
        ClassRepository = new ClassRepository(_context);
        CourseRepository = new CourseRepository(_context);
        PaymentMethodRepository = new PaymentMethodRepository(_context);
        CourseRegistrationRepository = new CourseRegistrationRepository(_context);
        PositionRepository = new PositionRepository(_context);
        ReceiptsExpensesRepository = new ReceiptsExpensesRepository(_context);
        StaffRepository = new StaffRepository(_context);
        StudentProgressRepository = new StudentProgressRepository(_context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }
}