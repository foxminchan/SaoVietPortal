using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    public void AddStudent(Student student);
    public void UpdateStudent(Student student);
    public void DeleteStudent(string id);
    public IEnumerable<Student> GetAllStudents();
    public Student? GetStudentById(string id);
}