using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Infrastructure.CQRS.Events.Commands;

namespace SaoViet.Portal.Application.Branch.Events;

public record UpdateBranchCommand(object Id, BranchDto BranchDto) : UpdateCommandBase<BranchDto>(Id);