using SaoViet.Portal.Domain.ValueObjects;
using SaoViet.Portal.Infrastructure.CQRS.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SaoViet.Portal.Application.Branch.DTOs;

[SwaggerSchema(
    Title = "Branch",
    Description = "Represents a data transfer object (DTO) for a branch.")]
public record BranchDto(string Id, string Name, Address? Address, string Phone, byte Status) : BaseModel;