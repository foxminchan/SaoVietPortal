using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Infrastructure.CQRS.Events.Queries;

namespace SaoViet.Portal.Application.Branch.Events;

public record GetBranchesQuery : GetQueryBase<BranchDto>;