using AutoMapper;
using FluentValidation;
using Lucene.Net.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.ValueObjects;
using System.Net.Mime;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<StudentController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Student> _validator;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService _luceneService;

    public StudentController(
        StudentService studentService,
        TransactionService transactionService,
        ILogger<StudentController> logger,
        IMapper mapper,
        IValidator<Student> validator,
        IRedisCacheService redisCacheService,
        ILuceneService luceneService
    )
    {
        _studentService = studentService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
        _luceneService = luceneService;
    }

    /// <summary>
    /// Get all students
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Student
    /// </remarks>
    /// <response code="200">Response the list of students</response>
    /// <response code="404">No student found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
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
    public ActionResult GetStudents()
    {
        try
        {
            return (_redisCacheService.GetOrSet("StudentData",
                    () => _studentService.GetAllStudents().ToList())) switch
            {
                { Count: > 0 } students => Ok(students),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting students");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find student by id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Student id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Student/{id}
    /// </remarks>
    /// <response code="200">Response the student</response>
    /// <response code="404">No student found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Student))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public ActionResult GetStudentById(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet("StudentData", () => _studentService.GetAllStudents().ToList())
                    .FirstOrDefault(s => s.studentId == id) switch
            {
                { } student => Ok(student),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting student by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find student by name
    /// </summary>
    /// <param name="name">Student name</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Student?name={name}
    /// </remarks>
    /// <response code="200">Response the list of students</response>
    /// <response code="404">No student found</response>
    [HttpGet("search")]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public ActionResult GetStudentByName(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromQuery] string name)
    {
        try
        {
            var studentDocuments = new List<Document>();

            var students = _studentService.GetAllStudents().ToList();

            if (!students.Any()) return NotFound();

            studentDocuments.AddRange(students.Select(student => new Document
            {
                new StringField("id", student.studentId, Field.Store.YES),
                new StringField("name", student.fullname, Field.Store.YES),
                new StringField("gender", student.gender ? "Nam" : "Nữ", Field.Store.YES),
                new StringField("address", student.address ?? string.Empty, Field.Store.YES),
                new StringField("dob", student.dob ?? string.Empty, Field.Store.YES),
                new StringField("pod", student.pod ?? string.Empty, Field.Store.YES),
                new StringField("occupation" , student.occupation ?? string.Empty, Field.Store.YES),
                new StringField("socialNetwork", student.socialNetwork?.GetString() ?? string.Empty, Field.Store.YES)
            }));

            _luceneService.Index(studentDocuments);

            return _luceneService.Search(name, 20).ToList() switch
            {
                { Count: > 0 } result => Ok(result),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting student by name");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new student
    /// </summary>
    /// <param name="student">Student object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Student
    ///     {
    ///         "fullname": "string",
    ///         "gender": true,
    ///         "address": "string",
    ///         "dob": "string",
    ///         "pod": "string",
    ///         "occupation": "string",
    ///         "socialNetwork": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new student successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Student id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(408)]
    [ProducesResponseType(409)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
    public ActionResult InsertStudent(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromBody] Student student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (student.studentId != null && _studentService.GetStudentById(student.studentId) != null)
                return Conflict();

            var newStudent = _mapper.Map<Domain.Entities.Student>(student);

            _transactionService.ExecuteTransaction(() => _studentService.AddStudent(newStudent));

            var students = _redisCacheService.GetOrSet("StudentData", () => _studentService.GetAllStudents().ToList());
            if (students.FirstOrDefault(s => s.studentId == newStudent.studentId) == null)
                students.Add(_mapper.Map<Domain.Entities.Student>(newStudent));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding student");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete student by id
    /// </summary>
    /// <param name="id">Student id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Student/{id}
    /// </remarks>
    /// <response code="200">Delete student successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="404">No student found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    public ActionResult DeleteStudent(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromRoute] string id)
    {
        try
        {
            if (_studentService.GetStudentById(id) == null)
                return NotFound();

            _transactionService.ExecuteTransaction(() => _studentService.DeleteStudent(id));

            if (_redisCacheService.GetOrSet("StudentData", () => _studentService.GetAllStudents().ToList()) is
                { Count: > 0 } students)
                students.RemoveAll(s => s.studentId == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting student");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update student
    /// </summary>
    /// <param name="student">Student object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Student
    ///     {
    ///         "fullname": "string",
    ///         "gender": true,
    ///         "address": "string",
    ///         "dob": "string",
    ///         "pod": "string",
    ///         "occupation": "string",
    ///         "socialNetwork": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update student successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    public ActionResult UpdateStudent(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromBody] Student student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (student.studentId != null && _studentService.GetStudentById(student.studentId) == null)
                return BadRequest();

            var updateStudent = _mapper.Map<Domain.Entities.Student>(student);
            _transactionService.ExecuteTransaction(() => _studentService.UpdateStudent(updateStudent));

            if (_redisCacheService.GetOrSet("StudentData", () => _studentService.GetAllStudents().ToList()) is
                { Count: > 0 } students)
                students[students.FindIndex(s => s.studentId == updateStudent.studentId)] = updateStudent;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating student");
            return StatusCode(500);
        }
    }
}