using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Domain.Primitives;

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
            return _redisCacheService
                    .GetOrSet(CacheKey,
                    () => _unitOfWork.CourseRegistrationRepository.GetAllCourseRegistrations().ToList()) switch
                {
                    { Count: > 0 } courseRegistrations 
                        => Ok(_mapper.Map<List<CourseRegistration>>(courseRegistrations)),
                    _ => NotFound()
                };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting course registrations");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find course registration by id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Course registration id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/CourseRegistration/{id}
    /// </remarks>
    /// <response code="200">Response the course registration</response>
    /// <response code="404">If no course registration is found</response>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(CourseRegistration))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetCourseRegistrationById([FromRoute] Guid id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, 
                        () => _unitOfWork.CourseRegistrationRepository
                            .GetAllCourseRegistrations().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
                {
                    { } courseRegistration => Ok(courseRegistration),
                    _ => NotFound()
                };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting course registration by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new course registration
    /// </summary>
    /// <param name="courseRegistration">Course registration object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/CourseRegistration
    ///     {
    ///         "Status": "string",
    ///         "RegisterDate": "dd/MM/yyyy",
    ///         "AppointmentDate": "dd/MM/yyyy",
    ///         "Fee": "float",
    ///         "DiscountAmount": "float",
    ///         "PaymentMethodId": "int",
    ///         "StudentId": "string",
    ///         "ClassId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new course registration successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="409">Course registration id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult InsertCourseRegistration([FromBody] CourseRegistration courseRegistration)
    {
        try
        {
            if (courseRegistration.Id != Guid.Empty)
                return BadRequest("Course registration id is auto generated");

            var validationResult = _validator.Validate(courseRegistration);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            var newCourseRegistration = _mapper.Map<Domain.Entities.CourseRegistration>(courseRegistration);

            _transactionService.ExecuteTransaction(() => _unitOfWork.CourseRegistrationRepository
                .AddCourseRegistration(newCourseRegistration));

            var courseRegistrations =
                _redisCacheService.GetOrSet(CacheKey, 
                    () => _unitOfWork.CourseRegistrationRepository.GetAllCourseRegistrations().ToList());
            if (courseRegistrations.FirstOrDefault(s => s.Id == newCourseRegistration.Id) is null)
                courseRegistrations.Add(_mapper.Map<Domain.Entities.CourseRegistration>(newCourseRegistration));

            return Created($"/api/v1/CourseRegistration/{newCourseRegistration.Id}", newCourseRegistration);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding course registration");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete course registration by id
    /// </summary>
    /// <param name="id">Course registration id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/CourseRegistration/{id}
    /// </remarks>
    /// <response code="200">Delete course registration successfully</response>
    /// <response code="404">If no course registration found</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteCourseRegistration([FromRoute] Guid id)
    {
        try
        {
            if (_unitOfWork.CourseRegistrationRepository.TryGetCourseRegistrationById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(
                () => _unitOfWork.CourseRegistrationRepository.DeleteCourseRegistration(id));

            if (_redisCacheService.GetOrSet(CacheKey,
                    () => _unitOfWork.CourseRegistrationRepository.GetAllCourseRegistrations().ToList()) is
                { Count: > 0 } courseRegistrations)
                courseRegistrations.RemoveAll(s => s.Id == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting course registration");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update course registration
    /// </summary>
    /// <param name="courseRegistration">Course registration object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/CourseRegistration
    ///     {
    ///         "Id": "guid",
    ///         "Status": "string",
    ///         "RegisterDate": "dd/MM/yyyy",
    ///         "AppointmentDate": "dd/MM/yyyy",
    ///         "Fee": "float",
    ///         "DiscountAmount": "float",
    ///         "PaymentMethodId": "int",
    ///         "StudentId": "string",
    ///         "ClassId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">update course registration successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If course registration id is not found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateCourseRegistration([FromBody] CourseRegistration courseRegistration)
    {
        try
        {
            var validationResult = _validator.Validate(courseRegistration);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (!_unitOfWork.CourseRegistrationRepository.TryGetCourseRegistrationById(courseRegistration.Id, out _))
                return NotFound();

            var updateCourseRegistration = _mapper.Map<Domain.Entities.CourseRegistration>(courseRegistration);
            _transactionService.ExecuteTransaction(
                () => _unitOfWork.CourseRegistrationRepository.UpdateCourseRegistration(updateCourseRegistration));

            if (_redisCacheService
                    .GetOrSet(CacheKey, 
                        () => _unitOfWork.CourseRegistrationRepository.GetAllCourseRegistrations().ToList()) is
                { Count: > 0 } courseRegistrations)
                courseRegistrations[courseRegistrations.FindIndex(s => s.Id == updateCourseRegistration.Id)] = updateCourseRegistration;

            return Created($"/api/v1/CourseRegistration/{updateCourseRegistration.Id}", updateCourseRegistration);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating course registration");
            return StatusCode(500);
        }
    }
}