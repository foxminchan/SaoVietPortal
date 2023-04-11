using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Application.Services;

public class StaffService
{
    private readonly IUnitOfWork _unitOfWork;

    public StaffService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public void AddStaff(Staff staff) => _unitOfWork.staffRepository.AddStaff(staff);

    public void UpdateStaff(Staff staff) => _unitOfWork.staffRepository.UpdateStaff(staff);

    public void DeleteStaff(string staffId) => _unitOfWork.staffRepository.DeleteStaff(staffId);

    public Staff? GetStaffById(string? staffId) => _unitOfWork.staffRepository.GetStaffById(staffId);

    public IEnumerable<Staff> GetStaff() => _unitOfWork.staffRepository.GetStaff();
}