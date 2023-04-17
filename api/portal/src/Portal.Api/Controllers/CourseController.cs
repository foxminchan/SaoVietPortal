using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.ValueObjects;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CourseController : ControllerBase
{
    private readonly CourseService _courseService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<CourseService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Course> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public CourseController(
        CourseService courseService,
        TransactionService transactionService,
        ILogger<CourseService> logger,
        IMapper mapper,
        IValidator<Course> validator,
        IRedisCacheService redisCacheService
    )
    {
        _courseService = courseService;
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
    public ActionResult GetCourses()
    {
        try
        {
            return (_redisCacheService.GetOrSet("CourseData",
                    () => _courseService.GetAllCourses().ToList())) switch
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
    public ActionResult GetCourseById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet("CourseData", () => _courseService.GetAllCourses().ToList())
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
    public ActionResult InsertCourse([FromBody] Course course)
    {
        try
        {
            var validationResult = _validator.Validate(course);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (course.courseId != null && _courseService.GetCourseById(course.courseId) != null)
                return Conflict();

            var newCourse = _mapper.Map<Domain.Entities.Course>(course);

            _transactionService.ExecuteTransaction(() => _courseService.AddCourse(newCourse));

            var positions =
                _redisCacheService.GetOrSet("CourseData", () => _courseService.GetAllCourses().ToList());

            if (positions.FirstOrDefault(s => s.courseId == newCourse.courseId) == null)
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
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no course is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult DeleteCourse([FromRoute] string id)
    {
        try
        {
            if (_courseService.GetCourseById(id) != null)
                return NotFound();

            _transactionService.ExecuteTransaction(() => _courseService.DeleteCourse(id));

            if (_redisCacheService.GetOrSet("CourseData", () => _courseService.GetAllCourses().ToList()) is
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
    public ActionResult UpdateCourse([FromBody] Course course)
    {
        try
        {
            var validationResult = _validator.Validate(course);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (course.courseId != null && _courseService.GetCourseById(course.courseId) == null)
                return NotFound();

            var updateCourse = _mapper.Map<Domain.Entities.Course>(course);
            _transactionService.ExecuteTransaction(() => _courseService.UpdateCourse(updateCourse));

            if (_redisCacheService.GetOrSet("CourseData", () => _courseService.GetAllCourses().ToList()) is
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