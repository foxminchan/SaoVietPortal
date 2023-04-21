using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class ClassService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClassService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IEnumerable<Class> GetAllClasses() => _unitOfWork.classRepository.GetAllClasses();

    public bool TryGetClassById(string classId, out Class? @class) 
        => _unitOfWork.classRepository.TryGetClassById(classId, out @class);

    public void AddClass(Class @class) => _unitOfWork.classRepository.AddClass(@class);

    public void DeleteClass(string id) => _unitOfWork.classRepository.DeleteClass(id);

    public void UpdateClass(Class @class) => _unitOfWork.classRepository.UpdateClass(@class);
}