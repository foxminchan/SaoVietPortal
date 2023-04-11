namespace Portal.Domain.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository studentRepository { get; }

    IBranchRepository branchRepository { get; }

    IClassRepository classRepository { get; }

    ICourseRepository courseRepository { get; }

    IPaymentMethodRepository paymentMethodRepository { get; }

    ICourseRegistrationRepository courseRegistrationRepository { get; }

    IPositionRepository positionRepository { get; }

    IReceiptsExpensesRepository receiptsExpensesRepository { get; }

    IStaffRepository staffRepository { get; }

    IStudentProgressRepository studentProgressRepository { get; }
}