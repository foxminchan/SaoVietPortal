﻿namespace SaoViet.Portal.Domain.Interfaces;

public interface IDomainEventContext
{
    public IEnumerable<IDomainEvent> GetDomainEvents();
}