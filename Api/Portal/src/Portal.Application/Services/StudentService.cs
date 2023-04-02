using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure;
using Portal.Infrastructure.Repositories;

namespace Portal.Application.Services;

public class StudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(ApplicationDbContext context) => _studentRepository = new StudentRepository(context);

    public IEnumerable<Student> GetAllStudents() => _studentRepository.GetAllStudents();

    public Student? GetStudentById(string id) => _studentRepository.GetStudentById(id);

    public void AddStudent(Student student) => _studentRepository.AddStudent(student);

    public void DeleteStudent(string id) => _studentRepository.DeleteStudent(id);
}