using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStudentProgressRepository : IRepository<StudentProgress>
{
    public void AddStudentProgress(StudentProgress studentProgress);

    public void UpdateStudentProgress(StudentProgress studentProgress);

    public void DeleteStudentProgress(Guid progressId);

    public bool TryGetStudentProgressById(Guid progressId, out StudentProgress? studentProgress);

    public IEnumerable<StudentProgress> GetStudentProgresses();
}