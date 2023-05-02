using FluentValidation;
using Portal.Api.Models;
using Portal.Domain.Interfaces.Common;
using System.Globalization;

namespace Portal.Api.Validations;

public class StaffValidator : AbstractValidator<StaffResponse>
{
    public StaffValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("StaffId is required")
            .MaximumLength(20).WithMessage("StaffId must not exceed 20 characters");
        RuleFor(x => x.Fullname)
            .NotEmpty().WithMessage("Fullname is required")
            .MaximumLength(50).WithMessage("Fullname must not exceed 50 characters");
        RuleFor(x => x.Dob)
            .Must(dobString =>
            {
                if (!DateTime.TryParseExact(dobString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dob))
                    return false;
                return dob < DateTime.Now;
            }).WithMessage("Date of birth must be in the past");
        RuleFor(x => x.Address)
            .MaximumLength(80).WithMessage("Address must not exceed 80 characters");
        RuleFor(x => x.ManagerId)
            .Must((_, managerId) => managerId is null
                                    || unitOfWork.StaffRepository.TryGetStaffById(managerId, out var _))
            .WithMessage("Manager with id {PropertyValue} does not exist");
        RuleFor(x => x.BranchId)
            .Must((_, branchId) => branchId is null
                                   || unitOfWork.BranchRepository.TryGetBranchById(branchId, out var _))
            .WithMessage("Branch with id {PropertyValue} does not exist");
        RuleFor(x => x.PositionId)
            .Must((_, positionId) => !positionId.HasValue
                                     || unitOfWork.PositionRepository.TryGetPositionById(positionId.Value, out var _))
            .WithMessage("Position with id {PropertyValue} does not exist");
    }
}