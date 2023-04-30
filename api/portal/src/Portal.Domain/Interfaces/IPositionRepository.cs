using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    public void AddPosition(Position position);

    public void UpdatePosition(Position position);

    public void DeletePosition(int id);

    public bool TryGetPositionById(int id, out Position? position);

    public IEnumerable<Position> GetAllPositions();
}