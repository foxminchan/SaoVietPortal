using MediatR;

namespace Portal.Shared.Core.Interfaces;

public interface IDomainEvent : INotification
{
    public DateTime OccurredOn { get; }
    public IDictionary<string, object> Metadata { get; }
}