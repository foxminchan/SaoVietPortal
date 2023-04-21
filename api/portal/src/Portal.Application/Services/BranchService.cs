using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class BranchService
{
    private readonly IUnitOfWork _unitOfWork;

    public BranchService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IEnumerable<Branch> GetAllBranches() => _unitOfWork.branchRepository.GetAllBranches();

    public bool TryGetBranchById(string id, out Branch? branch) 
        => _unitOfWork.branchRepository.TryGetBranchById(id, out branch);

    public void AddBranch(Branch branch) => _unitOfWork.branchRepository.AddBranch(branch);

    public void DeleteBranch(string id) => _unitOfWork.branchRepository.DeleteBranch(id);

    public void UpdateBranch(Branch branch) => _unitOfWork.branchRepository.UpdateBranch(branch);
}