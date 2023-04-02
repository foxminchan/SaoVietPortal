using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Portal.Infrastructure;

public interface IDatabaseFacade
{
    DatabaseFacade Database { get; }
}