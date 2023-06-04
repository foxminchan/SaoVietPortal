using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaoViet.Portal.Application.Branch.DTOs;
using SaoViet.Portal.Application.Branch.Events;
using Swashbuckle.AspNetCore.Annotations;

namespace SaoViet.Portal.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manager branch APIs")]
public class BranchController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get all branches",
        Description = """
            Retrieves information about all branches.

            ## Permission:

            This endpoint is publicly accessible.

            ## Sample request:

            ```http
            GET / api / v1 / branch HTTP / 1.1
            Host: localhost:8080
            Accept: application / json
            ```

            The API can be invoked using * curl* like below:

            ```bash
            curl -X GET "http://localhost:8080/api/v1/branch" -H "accept: application/json"
            ```
            """,
        OperationId = "GetBranches",
        Tags = new[] { "Branch" })]
    [SwaggerResponse(200, "The branches information was successfully retrieved.",
        typeof(List<BranchDto>))]
public async Task<IActionResult> GetBranches()
{
    var branches = await Mediator.Send(new GetBranchesQuery());
    return Ok(branches);
}

[HttpGet("{id}")]
[AllowAnonymous]
[SwaggerOperation(
    Summary = "Get branch by id",
    Description = """

        Retrieves information about branch by id.

            ## Permission:

        This endpoint is publicly accessible.

            ## Sample request:

            ```http

        GET / api / v1 / branch /{id} HTTP / 1.1
            Host: localhost: 8080
            Accept: application / json
            ```

            The API can be invoked using *curl * like below:

            ```bash
            curl -X GET "http://localhost:8080/api/v1/branch/{id}" -H "accept: application/json"
            ```
            """,
        OperationId = "GetBranchById",
        Tags = new[] { "Branch" })]
    [SwaggerResponse(200, "The branch information was successfully retrieved.",
        typeof(BranchDto))]
public async Task<IActionResult> GetBranchById([FromRoute] string id)
{
    var branch = await Mediator.Send(new GetBranchByIdQuery(id));
    return Ok(branch);
}
}