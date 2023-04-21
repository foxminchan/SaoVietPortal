using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class StudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IEnumerable<Student> GetAllStudents() => _unitOfWork.studentRepository.GetAllStudents();

    public bool TryGetStudentById(string id, out Student? student) 
        => _unitOfWork.studentRepository.TryGetStudentById(id, out student);

    public void AddStudent(Student student) => _unitOfWork.studentRepository.AddStudent(student);

    public void DeleteStudent(string id) => _unitOfWork.studentRepository.DeleteStudent(id);

    public void UpdateStudent(Student student) => _unitOfWork.studentRepository.UpdateStudent(student);
}