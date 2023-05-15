using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class PositionRepository : RepositoryBase<Position, PositionId>
{
    public PositionRepository(ApplicationDbContext context) : base(context)
    {
    }
}