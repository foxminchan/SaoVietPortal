using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class StudentProgressRepository : RepositoryBase<StudentProgress>, IStudentProgressRepository
{
    public StudentProgressRepository(ApplicationDbContext context) : base(context) { }

    public void AddStudentProgress(StudentProgress studentProgress) => Insert(studentProgress);

    public void UpdateStudentProgress(StudentProgress studentProgress) => Update(studentProgress);

    public void DeleteStudentProgress(Guid progressId) => Delete(x => x.progressId == progressId);

    public bool TryGetStudentProgressById(Guid progressId, out StudentProgress? studentProgress) 
        => TryGetById(progressId, out studentProgress);

    public IEnumerable<StudentProgress> GetStudentProgresses() => GetAll();
}