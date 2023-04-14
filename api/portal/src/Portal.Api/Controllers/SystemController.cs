using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Portal.Api.Extensions;
using Portal.Application.Cache;
using Portal.Application.Search;
using System.Net.Mime;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
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
    [ProducesResponseType(200, Type = typeof(JObject))]
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
    /// Clean cache
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System/cleanCache
    /// </remarks>
    [HttpGet("cleanCache")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult CleanCache()
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
    /// Clear index
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/System/clearIndex
    /// </remarks>
    [HttpGet("clearIndex")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult ClearIndex()
    {
        try
        {
            _luceneService.ClearAll();
            return Ok(new { message = "Index cleared" });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while clearing index");
            return StatusCode(500);
        }
    }
}