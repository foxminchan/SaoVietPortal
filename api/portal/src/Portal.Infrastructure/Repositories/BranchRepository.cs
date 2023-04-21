using Portal.Domain.Entities;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Repositories.Common;

namespace Portal.Infrastructure.Repositories;

public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context) { }

    public void AddBranch(Branch branch) => Insert(branch);

    public void UpdateBranch(Branch branch) => Update(branch);

    public void DeleteBranch(string id) => Delete(x => x.branchId == id);

    public IEnumerable<Branch> GetAllBranches() => GetAll();

    public bool TryGetBranchById(string id, out Branch? branch) => TryGetById(id, out branch);
}