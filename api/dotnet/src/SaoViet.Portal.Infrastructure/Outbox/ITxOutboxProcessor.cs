namespace SaoViet.Portal.Infrastructure.Outbox;

public interface ITxOutboxProcessor
{
    Task HandleAsync(Type integrationAssemblyType, CancellationToken cancellationToken = new());
}