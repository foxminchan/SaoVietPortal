using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SaoViet.Portal.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[ApiController]
public class BaseController : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}