using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Search;
using Portal.Application.Transaction;
using Portal.Domain.Entities;
using Portal.Domain.Enum;
using Portal.Domain.Interfaces.Common;
using Portal.Domain.Options;

namespace Portal.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class StudentController : ControllerBase
{
    private const string CacheKey = "StudentData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<StudentController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<StudentResponse> _validator;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService<StudentResponse> _luceneService;

    public StudentController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<StudentController> logger,
        IMapper mapper,
        IValidator<StudentResponse> validator,
        IRedisCacheService redisCacheService,
        ILuceneService<StudentResponse> luceneService)
    => (_unitOfWork, _transactionService, _logger, _mapper, _validator, _redisCacheService, _luceneService) =
        (unitOfWork, transactionService, logger, mapper, validator, redisCacheService, luceneService);

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
    [ProducesResponseType(200, Type = typeof(List<StudentResponse>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStudents()
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                        .GetAllStudents().ToList()) switch
            {
                { Count: > 0 } students => Ok(_mapper.Map<List<StudentResponse>>(students)),
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
    [ProducesResponseType(200, Type = typeof(StudentResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStudentById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                        .GetAllStudents().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } student => Ok(_mapper.Map<StudentResponse>(student)),
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
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(StudentResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStudentByName([FromQuery(Name = "name"), BindRequired] string name)
    {
        try
        {
            var students = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                    .GetAllStudents().ToList())
                .Select(_mapper.Map<StudentResponse>).ToList();

            if (!students.Any()) return NotFound();

            if (!_luceneService.IsExistIndex(students.First()))
                _luceneService.Index(students, nameof(LuceneOptions.Create));

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
    ///         "Id": "string",
    ///         "Fullname": "string",
    ///         "Gender": bool,
    ///         "Address": "string",
    ///         "Dob": "dd/MM/yyyy",
    ///         "Pod": "string",
    ///         "Occupation": "string",
    ///         "SocialNetwork": "json"
    ///     }
    /// </remarks>
    /// <response code="200">Add new student successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Student id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult InsertStudent([FromBody] StudentResponse student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (_unitOfWork.StudentRepository.TryGetStudentById(student.Id, out _))
                return Conflict();

            var newStudent = _mapper.Map<Student>(student);

            _transactionService.ExecuteTransaction(() => _unitOfWork.StudentRepository.AddStudent(newStudent));

            var students = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                    .GetAllStudents().ToList());

            if (students.FirstOrDefault(s => s.Id == newStudent.Id) is null)
                students.Add(_mapper.Map<Student>(newStudent));

            _luceneService
                .Index(students
                    .Select(_mapper.Map<StudentResponse>).ToList(), nameof(LuceneOptions.Create));

            _logger.LogInformation("Completed request {@RequestName}", nameof(InsertStudent));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{student.Id}"), student);
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
    /// <response code="404">If no student is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteStudent([FromRoute] string id)
    {
        try
        {
            if (!_unitOfWork.StudentRepository.TryGetStudentById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.StudentRepository.DeleteStudent(id));

            var students = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                    .GetAllStudents().ToList());

            if (students.FirstOrDefault(s => s.Id == id) is { } student)
                students.Remove(student);

            _luceneService
                .Index(students
                    .Select(_mapper.Map<StudentResponse>).ToList(), nameof(LuceneOptions.Delete));

            _logger.LogInformation("Completed request {@RequestName}", nameof(DeleteStudent));

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
    ///         "Id": "string",
    ///         "Fullname": "string",
    ///         "Gender": bool,
    ///         "Address": "string",
    ///         "Dob": "dd/MM/yyyy",
    ///         "Pod": "string",
    ///         "Occupation": "string",
    ///         "SocialNetwork": "json"
    ///     }
    /// </remarks>
    /// <response code="200">Update student successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateStudent([FromBody] StudentResponse student)
    {
        try
        {
            var validationResult = _validator.Validate(student);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (!_unitOfWork.StudentRepository.TryGetStudentById(student.Id, out _))
                return NotFound();

            var updateStudent = _mapper.Map<Student>(student);
            _transactionService.ExecuteTransaction(() => _unitOfWork.StudentRepository.UpdateStudent(updateStudent));

            var students = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StudentRepository
                    .GetAllStudents().ToList());

            if (students.FirstOrDefault(s => s.Id == updateStudent.Id) is { } data)
                students[students.IndexOf(data)] = updateStudent;

            _luceneService
                .Index(students
                    .Select(_mapper.Map<StudentResponse>).ToList(), nameof(LuceneOptions.Update));

            _logger.LogInformation("Completed request {@RequestName}", nameof(UpdateStudent));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{student.Id}"), student);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating student");
            return StatusCode(500);
        }
    }
}