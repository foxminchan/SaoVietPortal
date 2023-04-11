using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class PositionRepository : RepositoryBase<Position>, IPositionRepository
{
    public PositionRepository(ApplicationDbContext context) : base(context) { }

    public void AddPosition(Position position) => Insert(position);

    public void UpdatePosition(Position position) => Update(position);

    public void DeletePosition(int id) => Delete(x => x.positionId == id);

    public Position? GetPositionById(int? id) => GetById(id);

    public IEnumerable<Position> GetAllPositions() => GetAll();
}