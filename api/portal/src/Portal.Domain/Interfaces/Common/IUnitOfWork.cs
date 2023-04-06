namespace Portal.Domain.Interfaces.Common;

public interface IUnitOfWork : IDisposable
{
    IStudentRepository studentRepository { get; }
}