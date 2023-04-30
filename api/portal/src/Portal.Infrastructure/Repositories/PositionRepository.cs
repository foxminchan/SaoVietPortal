using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class PositionRepository : RepositoryBase<Position>, IPositionRepository
{
    public PositionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void AddPosition(Position position) => Insert(position);

    public void UpdatePosition(Position position) => Update(position);

    public void DeletePosition(int id) => Delete(x => x.Id == id);

    public bool TryGetPositionById(int id, out Position? position)
        => TryGetById(id, out position);

    public IEnumerable<Position> GetAllPositions() => GetAll();
}