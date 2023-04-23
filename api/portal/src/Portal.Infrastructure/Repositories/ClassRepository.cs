using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class ClassRepository : RepositoryBase<Class>, IClassRepository
{
    public ClassRepository(ApplicationDbContext context) : base(context) { }

    public void AddClass(Class @class) => Insert(@class);

    public void UpdateClass(Class @class) => Update(@class);

    public void DeleteClass(string id) => Delete(x => x.Id == id);

    public IEnumerable<Class> GetAllClasses() => GetAll();

    public bool TryGetClassById(string classId, out Class? @class) => TryGetById(classId, out @class);
}