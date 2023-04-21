using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class PositionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PositionService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddPosition(Position position) => _unitOfWork.positionRepository.AddPosition(position);

    public void UpdatePosition(Position position) => _unitOfWork.positionRepository.UpdatePosition(position);

    public void DeletePosition(int id) => _unitOfWork.positionRepository.DeletePosition(id);

    public bool TryGetPosition(int id, out Position? position) 
        => _unitOfWork.positionRepository.TryGetPositionById(id, out position);

    public IEnumerable<Position> GetAllPositions() => _unitOfWork.positionRepository.GetAllPositions();
}