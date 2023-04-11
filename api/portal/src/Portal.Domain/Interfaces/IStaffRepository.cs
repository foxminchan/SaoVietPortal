using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStaffRepository : IRepository<Staff>
{
    public void AddStaff(Staff staff);
    public void UpdateStaff(Staff staff);
    public void DeleteStaff(string staffId);
    public Staff? GetStaffById(string? staffId);
    public IEnumerable<Staff> GetStaff();
}