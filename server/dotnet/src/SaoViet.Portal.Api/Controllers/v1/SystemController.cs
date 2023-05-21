using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaoViet.Portal.Infrastructure.Cache.Redis;
using SaoViet.Portal.Infrastructure.Searching.Lucene;
using SaoViet.Portal.Infrastructure.System;
using SaoViet.Portal.Infrastructure.System.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SaoViet.Portal.Api.Controllers.v1;

[ApiVersion("1.0")]
[SwaggerTag("Manager server host APIs")]
public class SystemController : BaseController
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService<object> _luceneService;

    public SystemController(
        IConfiguration config,
        IWebHostEnvironment env,
        IRedisCacheService redisCacheService,
        ILuceneService<object> luceneService)
    => (_config, _env, _redisCacheService, _luceneService)
        = (config, env, redisCacheService, luceneService);

    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get platform information",
        Description = """
            Retrieves information about the current platform, including its name, version, and other relevant details.

            ## Permission:

            This endpoint is publicly accessible.

            ## Sample request:

            ```http
            GET / api / v1 / system HTTP / 1.1
            Host: localhost:8080
            Accept: application / json
            ```

            The API can be invoked using * curl* like below:

            ```bash
            curl -X GET "http://localhost:8080/api/v1/system" -H "accept: application/json"
            ```
            """,
        OperationId = "GetPlatform",
        Tags = new[] { "System" })]
    [SwaggerResponse(200, "The platform information was successfully retrieved.", typeof(Platform))]
public IActionResult GetPlatform()
        => Ok(_config.GetPlatform(_env));

[HttpGet("status")]
[AllowAnonymous]
[SwaggerOperation(
    Summary = "Get server status",
    Description = """

        Retrieves information about the cpu and memory usage of the current server.

            ## Permission:

        This endpoint is publicly accessible.

            ## Sample request:

            ```http

        GET / api / v1 / system / status HTTP / 1.1

        Host: localhost:8080

        Accept: application / json
            ```


        The API can be invoked using * curl*like below:

            ```bash
            curl -X GET "http://localhost:8080/api/v1/system/status" -H "accept: application/json"
            ```
            """,
        OperationId = "GetServerStatus",
        Tags = new[] { "System" })]
    [SwaggerResponse(200, "The status information was successfully retrieved.", typeof(Server))]
public IActionResult GetServerStatus()
        => Ok(PlatformExtension.GetPlatformStatus(_env));

[HttpPost("clear-cache")]
[Authorize(Policy = "Developer")]
[SwaggerOperation(
    Summary = "Get server status",
    Description = """

        Clear all cache in Redis.

            ## Permission:

        This endpoint is restricted to policy * *Developer * *.Only users with this policy can access this endpoint.

            ## Sample request:

            ```http

        POST / api / v1 / system / clear - cache HTTP / 1.1

        Host: localhost:8080

        Accept: application / json

        Authorization: Bearer {token}
            ```

            The API can be invoked using *curl * like below:

            ```bash
            curl -X POST "http://localhost:8080/api/v1/system/clear-cache" -H "accept: application/json"
            ```
            """,
        OperationId = "ClearCache",
        Tags = new[] { "System" })]
    [SwaggerResponse(200, "The cache was successfully cleared.")]
public IActionResult ClearCache()
{
    _redisCacheService.Reset();
    return Ok(new { Message = "Cache cleaned" });
}

[HttpPost("clear-index")]
[Authorize(Policy = "Developer")]
[SwaggerOperation(
    Summary = "Get server status",
    Description = """

        Clear all Lucene index directory.

            ## Permission:

        This endpoint is restricted to policy * *Developer * *.Only users with this policy can access this endpoint.

            ## Sample request:

            ```http

        POST / api / v1 / system / clear - index HTTP / 1.1

        Host: localhost:8080

        Accept: application / json

        Authorization: Bearer {token}
            ```

            The API can be invoked using *curl * like below:

            ```bash
            curl -X POST "http://localhost:8080/api/v1/system/clear-index" -H "accept: application/json"
            ```
            """,
        OperationId = "ClearIndex",
        Tags = new[] { "System" })]
    [SwaggerResponse(200, "The index was successfully cleared.")]
public IActionResult ClearIndex()
{
    _luceneService.ClearAll();
    return Ok(new { Message = "Index cleared" });
}
}