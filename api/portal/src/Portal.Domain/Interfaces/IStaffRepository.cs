using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;

namespace Portal.Domain.Interfaces;

public interface IStaffRepository : IRepository<Staff>
{
    public void AddStaff(Staff staff);
    public void UpdateStaff(Staff staff);
    public void DeleteStaff(string staffId);
    public bool TryGetStaffById(string staffId, out Staff? staff);
    public IEnumerable<Staff> GetStaff();
}