using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Infrastructure.CQRS.Events.Commands;

namespace SaoViet.Portal.Application.Branch.Events;

public record CreateBranchCommand(BranchDto BranchDto) : CreateCommandBase<BranchDto>;