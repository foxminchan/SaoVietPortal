using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<ApplicationUser>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public IQueryable<string?> GetClaims(string userId)
    {
        var userClaims = Context.UserClaims.Where(c => c.UserId == userId);
        return userClaims.Select(c => c.ClaimValue);
    }

    public IQueryable<string?> GetRoles(string userId)
    {
        var userRoles = Context.UserRoles.Where(r => r.UserId == userId);
        var roles = Context.Roles.Where(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id));
        return roles.Select(r => r.Name);
    }

    public IQueryable<string?> GetRoleClaims(string userId)
    {
        var userRoles = Context.UserRoles.Where(r => r.UserId == userId);
        var roles = Context.Roles.Where(r => userRoles.Select(ur => ur.RoleId).Contains(r.Id));
        var roleClaims = Context.RoleClaims.Where(c => roles.Select(r => r.Id).Contains(c.RoleId));
        return roleClaims.Select(c => c.ClaimValue);
    }
}
