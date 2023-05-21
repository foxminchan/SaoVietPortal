namespace SaoViet.Portal.Application.Staff.DTO;

public record StaffDto(
    string Id,
    string Fullname,
    string Dob,
    string Address,
    string Dsw,
    int? PositionId,
    string BranchId,
    string ManagerId);