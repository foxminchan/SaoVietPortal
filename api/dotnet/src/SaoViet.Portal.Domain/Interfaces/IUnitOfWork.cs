namespace SaoViet.Portal.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public void Commit();
}