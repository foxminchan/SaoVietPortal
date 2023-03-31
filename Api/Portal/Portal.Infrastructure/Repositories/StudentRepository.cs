using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context) { }

    public void AddStudent(Student student) => Insert(student);

    public void DeleteStudent(string id) => Delete(x => x.studentId == id);

    public void UpdateStudent(Student student) => Update(student);

    public IEnumerable<Student> GetAllStudents() => GetAll();

    public Student? GetStudentById(string id) => GetById(id);
}