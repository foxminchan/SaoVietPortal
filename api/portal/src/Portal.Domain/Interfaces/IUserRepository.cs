using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IUserRepository : IRepository<ApplicationUser>
{
    public IQueryable<string?> GetRoles(string userId);
    public IQueryable<string?> GetClaims(string userId);
    public IQueryable<string?> GetRoleClaims(string userId);
}