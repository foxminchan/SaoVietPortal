namespace Portal.Api.Models;

public record Class(
    string Id,
    string StartDate,
    string EndDate,
    float Fee,
    string CourseId,
    string BranchId);