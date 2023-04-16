using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Transaction;
using System.Net.Mime;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<CourseService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Course> _validator;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService _luceneService;

    public CourseController(
        CourseService courseService,
        TransactionService transactionService,
        ILogger<CourseService> logger,
        IMapper mapper,
        IValidator<Course> validator,
        IRedisCacheService redisCacheService,
        ILuceneService luceneService
    )
    {
        _courseService = courseService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
        _luceneService = luceneService;
    }

    /// <summary>
    /// Get all courses
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Course
    /// </remarks>
    /// <response code="200">Returns all courses</response>
    /// <response code="404">If no courses are found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Course>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetCourses()
    {
        try
        {
            return (_redisCacheService.GetOrSet("CourseData",
                    () => _courseService.GetAllCourses().ToList())) switch
            {
                { Count: > 0 } courses => Ok(courses),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting courses");
            return StatusCode(500);
        }
    }


}