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
public class CourseController : ControllerBase
{
    private const string CACHE_KEY = "CourseData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<CourseController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Course> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public CourseController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<CourseController> logger,
        IMapper mapper,
        IValidator<Course> validator,
        IRedisCacheService redisCacheService
    )
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
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
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetCourses()
    {
        try
        {
            return (_redisCacheService
                    .GetOrSet(CACHE_KEY, () => _unitOfWork.courseRepository.GetAllCourses().ToList())) switch
            {
                { Count: > 0 } courses => Ok(_mapper.Map<List<Course>>(courses)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting courses");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find course by id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Course id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Course/{id}
    /// </remarks>
    /// <response code="200">Response the course</response>
    /// <response code="404">No course found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Course))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetCourseById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CACHE_KEY, () => _unitOfWork.courseRepository.GetAllCourses().ToList())
                    .FirstOrDefault(s => s.courseId == id) switch
            {
                { } course => Ok(course),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting course by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add a new course
    /// </summary>
    /// <param name="course">Course object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Course
    ///     {
    ///         "courseId": "string",
    ///         "courseName": "string",
    ///         "description": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new course successfully</response>
    /// <response code="400">The course is invalid</response>
    /// <response code="409">Course already exists</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult InsertCourse([FromBody] Course course)
    {
        try
        {
            var validationResult = _validator.Validate(course);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (course.courseId is not null && _unitOfWork.courseRepository.TryGetCourseById(course.courseId, out _))
                return Conflict();

            var newCourse = _mapper.Map<Domain.Entities.Course>(course);

            _transactionService.ExecuteTransaction(() => _unitOfWork.courseRepository.AddCourse(newCourse));

            var positions =
                _redisCacheService.GetOrSet(CACHE_KEY, () => _unitOfWork.courseRepository.GetAllCourses().ToList());

            if (positions.FirstOrDefault(s => s.courseId == newCourse.courseId) is null)
                positions.Add(_mapper.Map<Domain.Entities.Course>(newCourse));

            return Ok();

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while inserting course");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete course by id
    /// </summary>
    /// <param name="id">Course id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Course/{id}
    /// </remarks>
    /// <response code="200">Delete course successfully</response>
    /// <response code="404">If no course is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteCourse([FromRoute] string id)
    {
        try
        {
            if (!_unitOfWork.courseRepository.TryGetCourseById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.courseRepository.DeleteCourse(id));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _unitOfWork.courseRepository.GetAllCourses().ToList()) is
                { Count: > 0 } courses)
                courses.RemoveAll(s => s.courseId == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting course");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update course
    /// </summary>
    /// <param name="course">Course object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Course
    ///     {
    ///         "courseId": "string",
    ///         "courseName": "string",
    ///         "description": "string"
    ///     }
    /// </remarks>
    /// <response code="200">update course successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If course id is not found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateCourse([FromBody] Course course)
    {
        try
        {
            var validationResult = _validator.Validate(course);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (course.courseId is not null && !_unitOfWork.courseRepository.TryGetCourseById(course.courseId, out _))
                return NotFound();

            var updateCourse = _mapper.Map<Domain.Entities.Course>(course);
            _transactionService.ExecuteTransaction(() => _unitOfWork.courseRepository.UpdateCourse(updateCourse));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _unitOfWork.courseRepository.GetAllCourses().ToList()) is
                { Count: > 0 } courses)
                courses[courses.FindIndex(s => s.courseId == updateCourse.courseId)] = updateCourse;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating course");
            return StatusCode(500);
        }
    }
}