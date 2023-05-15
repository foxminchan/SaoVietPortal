using MediatR;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Domain.DomainEvents;

public class EventWrapper : INotification
{
    public EventWrapper(IDomainEvent domainEvent) => DomainEvent = domainEvent;

    public IDomainEvent DomainEvent { get; }
}