using AutoMapper;
using FluentValidation;
using Lucene.Net.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.Primitives;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class StudentController : ControllerBase
{
    private const string CACHE_KEY = "StudentData";
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
    /// <response code="404">If no students are found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStudents()
    {
        try
        {
            return (_redisCacheService.GetOrSet(CACHE_KEY,
                    () => _studentService.GetAllStudents().ToList())) switch
            {
                { Count: > 0 } students => Ok(_mapper.Map<List<Student>>(students)),
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
    /// <response code="404">If no student is found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Student))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStudentById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CACHE_KEY, () => _studentService.GetAllStudents().ToList())
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
    /// <response code="404">If no students are found</response>
    [HttpGet("search")]
    [ProducesResponseType(200, Type = typeof(Student))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStudentByName([FromQuery(Name = "name"), BindRequired] string name)
    {
        try
        {
            var students = _redisCacheService
                .GetOrSet(CACHE_KEY, () => _studentService.GetAllStudents().ToList())
                .Select(_mapper.Map<Student>).ToList();

            if (!students.Any()) return NotFound();

            var propertyIndex = new Dictionary<string, List<Document>>();

            foreach (var student in students)
            {
                foreach (var property in student.GetType().GetProperties())
                {
                    if (!propertyIndex.ContainsKey(property.Name))
                        propertyIndex.Add(property.Name, new List<Document>());

                    var value = property.GetValue(student, null);

                    if (value is null) continue;

                    var document = new Document
                        { new StringField(property.Name, value.ToString(), Field.Store.YES) };

                    propertyIndex[property.Name].Add(document);
                }
            }

            _luceneService.Index(propertyIndex);

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
    ///         "studentId": "string",
    ///         "fullname": "string",
    ///         "gender": bool,
    ///         "address": "string",
    ///         "dob": "dd/MM/yyyy",
    ///         "pod": "string",
    ///         "occupation": "string",
    ///         "socialNetwork": "json"
    ///     }
    /// </remarks>
    /// <response code="200">Add new student successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Student id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public ActionResult InsertStudent([FromBody] Student student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (student.studentId is not null && _studentService.TryGetStudentById(student.studentId, out _))
                return Conflict();

            var newStudent = _mapper.Map<Domain.Entities.Student>(student);

            _transactionService.ExecuteTransaction(() => _studentService.AddStudent(newStudent));

            var students = _redisCacheService.GetOrSet(CACHE_KEY, () => _studentService.GetAllStudents().ToList());
            if (students.FirstOrDefault(s => s.studentId == newStudent.studentId) is null)
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
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no student is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult DeleteStudent([FromRoute] string id)
    {
        try
        {
            if (!_studentService.TryGetStudentById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _studentService.DeleteStudent(id));

            if (_redisCacheService
                    .GetOrSet(CACHE_KEY, () => _studentService.GetAllStudents().ToList())
                is { Count: > 0 } students)
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
    ///         "studentId": "string",
    ///         "fullname": "string",
    ///         "gender": bool,
    ///         "address": "string",
    ///         "dob": "dd/MM/yyyy",
    ///         "pod": "string",
    ///         "occupation": "string",
    ///         "socialNetwork": "json"
    ///     }
    /// </remarks>
    /// <response code="200">Update student successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult UpdateStudent([FromBody] Student student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (student.studentId is not null && !_studentService.TryGetStudentById(student.studentId, out _))
                return NotFound();

            var updateStudent = _mapper.Map<Domain.Entities.Student>(student);
            _transactionService.ExecuteTransaction(() => _studentService.UpdateStudent(updateStudent));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _studentService.GetAllStudents().ToList()) is
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