using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Extensions;
using Portal.Application.Cache;
using Portal.Application.Search;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class SystemController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SystemController> _logger;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService _luceneService;

    public SystemController(
        IConfiguration config,
        IWebHostEnvironment env,
        ILogger<SystemController> logger,
        IRedisCacheService redisCacheService,
        ILuceneService luceneService)
    {
        _config = config;
        _env = env;
        _logger = logger;
        _redisCacheService = redisCacheService;
        _luceneService = luceneService;
    }

    /// <summary>
    /// Get platform information
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System
    /// </remarks>
    /// <response code="200">Returns platform information</response>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult GetPlatform()
    {
        try
        {
            var platform = _config.GetPlatform(_env);
            return Ok(platform);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting platform information");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Clear cache data
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System/cleanCache
    /// </remarks>
    /// <response code="200">Clean cache is successful</response>
    [HttpGet("clearCache")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult ClearCache()
    {
        try
        {
            _redisCacheService.Reset();
            return Ok(new { message = "Cache cleaned" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while cleaning cache");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Clear index files
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System/clearIndex
    /// </remarks>
    /// <response code="200">Clean index is successful</response>
    [HttpGet("clearIndex")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult ClearIndex()
    {
        try
        {
            _luceneService.ClearAll();
            return Ok(new { message = "Index cleared" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while cleaning index");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Clear logs file
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System/clearLogsFile
    /// </remarks>
    /// <response code="200">Clean logs file is successful</response>
    [HttpGet("clearLogsFile")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public IActionResult ClearLogsFile()
    {
        try
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "*.log");

            Directory.GetFiles(path).ToList().ForEach(System.IO.File.Delete);

            return Ok(new { message = "Logs file cleaned" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while cleaning logs file");
            return StatusCode(500);
        }
    }
}