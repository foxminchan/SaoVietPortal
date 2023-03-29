using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        public Student? GetById(string id);
    }
}
