namespace Portal.Domain.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository studentRepository { get; }

    IBranchRepository branchRepository { get; }

    IClassRepository classRepository { get; }

    ICourseRepository courseRepository { get; }

    IPaymentMethodRepository paymentMethodRepository { get; }
}