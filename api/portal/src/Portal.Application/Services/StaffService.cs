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

    public bool TryGetStaffById(string staffId, out Staff? staff)
        => _unitOfWork.staffRepository.TryGetStaffById(staffId, out staff);

    public IEnumerable<Staff> GetStaff() => _unitOfWork.staffRepository.GetStaff();
}