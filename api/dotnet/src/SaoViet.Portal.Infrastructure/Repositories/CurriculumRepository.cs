using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CurriculumRepository : RepositoryBase<Curriculum, CurriculumId>
{
    public CurriculumRepository(ApplicationDbContext context) : base(context)
    {
    }
}