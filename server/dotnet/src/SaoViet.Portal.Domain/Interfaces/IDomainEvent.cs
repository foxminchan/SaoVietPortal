using MediatR;

namespace SaoViet.Portal.Domain.Interfaces;

public interface IDomainEvent : INotification
{
    public DateTime CreatedAt { get; }
    public IDictionary<string, object?> MetaData { get; }
}