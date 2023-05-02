namespace Portal.Api.Models;

public record ClassResponse(
    string Id,
    string StartDate,
    string EndDate,
    float Fee,
    string CourseId,
    string BranchId);