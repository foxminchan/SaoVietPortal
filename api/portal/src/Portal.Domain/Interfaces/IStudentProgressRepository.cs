using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStudentProgressRepository : IRepository<StudentProgress>
{
    public void AddStudentProgress(StudentProgress studentProgress);
    public void UpdateStudentProgress(StudentProgress studentProgress);
    public void DeleteStudentProgress(Guid progressId);
    public StudentProgress? GetStudentProgress(Guid? progressId);
    public IEnumerable<StudentProgress> GetStudentProgresses();
}