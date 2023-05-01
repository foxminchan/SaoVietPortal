using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;
using Portal.Domain.Options;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class StudentProgressController : ControllerBase
{
    private const string CacheKey = "StudentProgressData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<StudentProgressController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<StudentProgress> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public StudentProgressController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<StudentProgressController> logger,
        IMapper mapper,
        IValidator<StudentProgress> validator,
        IRedisCacheService redisCacheService)
        => (_unitOfWork, _transactionService, _logger, _mapper, _validator, _redisCacheService) =
            (unitOfWork, transactionService, logger, mapper, validator, redisCacheService);

    /// <summary>
    /// Get all student progress
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/StudentProgress
    /// </remarks>
    /// <response code="200">Response the list of student progress</response>
    /// <response code="404">If no student progress found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<StudentProgress>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStudentProgress()
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentProgressRepository
                        .GetStudentProgresses().ToList()) switch
            {
                { Count: > 0 } studentProgresses => Ok(_mapper
                    .Map<List<ReceiptsExpenses>>(studentProgresses)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting student progress");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get student progress by id
    /// </summary>
    /// <param name="id">Student progress id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/StudentProgress/{id}
    /// </remarks>
    /// <response code="200">Response the student progress</response>
    /// <response code="404">If no student progress found</response>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(StudentProgress))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetReceiptsExpensesById([FromRoute] Guid id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentProgressRepository
                        .GetStudentProgresses().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } studentProgress => Ok(_mapper.Map<StudentProgress>(studentProgress)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting student progress by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new student progress
    /// </summary>
    /// <param name="studentProgress">Student progress object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/StudentProgress
    ///     {
    ///         "Id": "Guid",
    ///         "LessonName": "string",
    ///         "LessonContent": "string",
    ///         "LessonDate": "string",
    ///         "Status": "string",
    ///         "LessonRating": int,
    ///         "StaffId": "string",
    ///         "StudentId": "string",
    ///         "ClassId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new student progress successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="409">Student progress id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    [ProducesDefaultResponseType]
    public IActionResult InsertStudentProgress([FromBody] StudentProgress studentProgress)
    {
        try
        {
            var validationResult = _validator.Validate(studentProgress);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (_unitOfWork.StudentProgressRepository.TryGetStudentProgressById(studentProgress.Id, out _))
                return Conflict();

            var newStudentProgress = _mapper.Map<Domain.Entities.StudentProgress>(studentProgress);

            _transactionService.ExecuteTransaction(
                () => _unitOfWork.StudentProgressRepository.AddStudentProgress(newStudentProgress));

            var studentProgresses = _redisCacheService
                .GetOrSet(CacheKey,
                    () => _unitOfWork.StudentProgressRepository.GetStudentProgresses().ToList());

            if (studentProgresses.FirstOrDefault(s => s.Id == newStudentProgress.Id) is null)
                studentProgresses.Add(_mapper.Map<Domain.Entities.StudentProgress>(newStudentProgress));

            _logger.LogInformation("Completed request {@RequestName}", nameof(InsertStudentProgress));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{newStudentProgress.Id}"), newStudentProgress);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding student progress");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete student progress by id
    /// </summary>
    /// <param name="id">Student progress id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/StudentProgress/{id}
    /// </remarks>
    /// <response code="200">Delete student progress successfully</response>
    /// <response code="404">If no student progress found</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteStudentProgress([FromRoute] Guid id)
    {
        try
        {
            if (_unitOfWork.StudentProgressRepository.TryGetStudentProgressById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(
                () => _unitOfWork.StudentProgressRepository.DeleteStudentProgress(id));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentProgressRepository
                        .GetStudentProgresses().ToList()) is { Count: > 0 } studentProgresses)
                studentProgresses.RemoveAll(s => s.Id == id);

            _logger.LogInformation("Completed request {@RequestName}", nameof(DeleteStudentProgress));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting student progress");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update student progress
    /// </summary>
    /// <param name="studentProgress">Student progress object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/StudentProgress
    ///     {
    ///         "Id": "Guid",
    ///         "LessonName": "string",
    ///         "LessonContent": "string",
    ///         "LessonDate": "string",
    ///         "Status": "string",
    ///         "LessonRating": int,
    ///         "StaffId": "string",
    ///         "StudentId": "string",
    ///         "ClassId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update student progress successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no student progress found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateStudentProgress([FromBody] StudentProgress studentProgress)
    {
        try
        {
            var validationResult = _validator.Validate(studentProgress);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (!_unitOfWork.StudentProgressRepository.TryGetStudentProgressById(studentProgress.Id, out _))
                return NotFound();

            var updateStudentProgress = _mapper.Map<Domain.Entities.StudentProgress>(studentProgress);
            _transactionService.ExecuteTransaction(
                () => _unitOfWork.StudentProgressRepository.UpdateStudentProgress(updateStudentProgress));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StudentProgressRepository
                        .GetStudentProgresses().ToList()) is
                { Count: > 0 } studentProgresses)
                studentProgresses[studentProgresses
                    .FindIndex(s => s.Id == updateStudentProgress.Id)] = updateStudentProgress;

            _logger.LogInformation("Completed request {@RequestName}", nameof(UpdateStudentProgress));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{updateStudentProgress.Id}"), updateStudentProgress);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating student progress");
            return StatusCode(500);
        }
    }
}