using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IBranchRepository : IRepository<Branch>
{
    public void AddBranch(Branch branch);

    public void UpdateBranch(Branch branch);

    public void DeleteBranch(string id);

    public IEnumerable<Branch> GetAllBranches();

    public bool TryGetBranchById(string id, out Branch? branch);
}