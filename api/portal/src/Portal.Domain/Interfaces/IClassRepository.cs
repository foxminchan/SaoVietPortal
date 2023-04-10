using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IClassRepository : IRepository<Class>
{
    public void AddClass(Class @class);
    public void UpdateClass(Class @class);
    public void DeleteClass(string id);
    public IEnumerable<Class> GetAllClasses();
    public Class? GetClassById(string id);
}