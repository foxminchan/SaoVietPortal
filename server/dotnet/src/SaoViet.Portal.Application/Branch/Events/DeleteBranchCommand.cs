using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Infrastructure.CQRS.Events.Commands;

namespace SaoViet.Portal.Application.Branch.Events;

public record DeleteBranchCommand(object Id) : DeleteCommandBase<BranchDto>(Id);