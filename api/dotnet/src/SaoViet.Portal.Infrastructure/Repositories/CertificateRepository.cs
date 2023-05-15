using SaoViet.Portal.Domain.Entities;
using SaoViet.Portal.Infrastructure.Persistence;

namespace SaoViet.Portal.Infrastructure.Repositories;

public class CertificateRepository : RepositoryBase<Certificate, CertificateId>
{
    public CertificateRepository(ApplicationDbContext context) : base(context)
    {
    }
}