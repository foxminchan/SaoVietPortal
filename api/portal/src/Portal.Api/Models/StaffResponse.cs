namespace Portal.Api.Models;

public record StaffResponse(
    string Id,
    string Fullname,
    string Dob,
    string Address,
    string Dsw,
    int? PositionId,
    string BranchId,
    string ManagerId);