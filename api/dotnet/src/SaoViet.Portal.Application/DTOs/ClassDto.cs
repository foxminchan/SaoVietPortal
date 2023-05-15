namespace SaoViet.Portal.Application.DTOs;

public record ClassDto(
    string Id,
    string StartDate,
    string EndDate,
    float Fee,
    string CourseId,
    string BranchId);