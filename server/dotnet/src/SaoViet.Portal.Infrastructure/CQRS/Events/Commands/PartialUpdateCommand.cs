using SaoViet.Portal.Infrastructure.CQRS.Models;

namespace SaoViet.Portal.Infrastructure.CQRS.Events.Commands;

public record PartialUpdateCommand<T>(object Id, IDictionary<string, object> Field) : UpdateCommandBase<T>(Id)
    where T : BaseModel;