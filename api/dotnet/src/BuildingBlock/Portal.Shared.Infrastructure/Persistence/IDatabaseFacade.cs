using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Portal.Shared.Infrastructure.Persistence;

public interface IDatabaseFacade
{
    public DatabaseFacade Database { get; }
}