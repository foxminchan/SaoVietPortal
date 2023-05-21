using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SaoViet.Portal.Infrastructure.Persistence;

public interface IDatabaseFacade
{
    public DatabaseFacade Database { get; }
}