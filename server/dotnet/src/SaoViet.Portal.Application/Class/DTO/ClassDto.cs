namespace SaoViet.Portal.Application.Class.DTO;

public record ClassDto(
    string Id,
    string StartDate,
    string EndDate,
    float Fee,
    string CourseId,
    string BranchId);