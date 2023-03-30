using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStudentRepository : IGenericRepository<Student>
{
    public void AddStudent(Student student);
    public void UpdateStudent(Student student);
    public void DeleteStudent(Student student);
    public IEnumerable<Student> GetAllStudents();
    public Student? GetStudentById(string id);
}