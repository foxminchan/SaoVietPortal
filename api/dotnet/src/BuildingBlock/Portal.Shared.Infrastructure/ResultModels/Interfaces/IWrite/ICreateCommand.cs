using Portal.Shared.Core.Interfaces;
using Portal.Shared.Infrastructure.ResultModels.Interfaces.Common;

namespace Portal.Shared.Infrastructure.ResultModels.Interfaces.IWrite;

public interface ICreateCommand : ICommand, ITxRequest
{
}