using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class ClassRepository : RepositoryBase<Class>, IClassRepository
{
    public ClassRepository(ApplicationDbContext context) : base(context) { }

    public void AddClass(Class @class) => Insert(@class);

    public void UpdateClass(Class @class) => Update(@class);

    public void DeleteClass(string id) => Delete(x => x.classId == id);

    public IEnumerable<Class> GetAllClasses() => GetAll();

    public Class? GetClassById(string id) => GetById(id);
}