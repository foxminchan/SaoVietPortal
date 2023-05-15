using SaoViet.Portal.Domain.ValueObjects;

namespace SaoViet.Portal.Application.Branch.DTO;

public record BranchDto(
    string Id,
    string Name,
    Address? Address,
    string Phone);