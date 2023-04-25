using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CourseRegistrationController : ControllerBase
{
    private const string CacheKey = "CourseRegistrationData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<CourseRegistrationController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CourseRegistration> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public CourseRegistrationController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<CourseRegistrationController> logger,
        IMapper mapper,
        IValidator<CourseRegistration> validator,
        IRedisCacheService redisCacheService)
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
    }

    /// <summary>
    /// Get all course registrations
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/CourseRegistration
    /// </remarks>
    /// <response code="200">Returns the list of  course registrations</response>
    /// <response code="404">If no course registrations are found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<CourseRegistration>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetCourseRegistrations()
    {
        try
        {
            return (_redisCacheService.GetOrSet(CacheKey,
                    () => _unitOfWork.CourseRegistrationRepository
                        .GetAllCourseRegistrations().ToList())) switch
            {
                { Count: > 0 } courseRegistrations => Ok(_mapper
                    .Map<List<CourseRegistration>>(courseRegistrations)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting course registrations");
            return StatusCode(500);
        }
    }
}