namespace SaoViet.Portal.Application.DTOs;

public record StaffDto(
    string Id,
    string Fullname,
    string Dob,
    string Address,
    string Dsw,
    int? PositionId,
    string BranchId,
    string ManagerId);