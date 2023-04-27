namespace Portal.Domain.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    public IStudentRepository StudentRepository { get; }

    public IBranchRepository BranchRepository { get; }

    public IClassRepository ClassRepository { get; }

    public ICourseRepository CourseRepository { get; }

    public IPaymentMethodRepository PaymentMethodRepository { get; }

    public ICourseRegistrationRepository CourseRegistrationRepository { get; }

    public IPositionRepository PositionRepository { get; }

    public IReceiptsExpensesRepository ReceiptsExpensesRepository { get; }

    public IStaffRepository StaffRepository { get; }

    public IStudentProgressRepository StudentProgressRepository { get; }
}