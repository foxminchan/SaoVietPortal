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
public class ClassController : ControllerBase
{
    private const string CacheKey = "ClassData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<ClassController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Class> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public ClassController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<ClassController> logger,
        IMapper mapper,
        IValidator<Class> validator,
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
    /// Get all classes
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Class
    /// </remarks>
    /// <response code="200">Returns all classes</response>
    /// <response code="404">If no classes found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(typeof(List<Class>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetClasses()
    {
        try
        {
            return _redisCacheService.GetOrSet(CacheKey,
                    () => _unitOfWork.ClassRepository.GetAllClasses().ToList()) switch
            {
                { Count: > 0 } classes => Ok(_mapper.Map<List<Class>>(classes)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting classes");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find class by id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Class id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Class/{id}
    /// </remarks>
    /// <response code="200">Response the class</response>
    /// <response code="404">If no class is found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Class))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetClassById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ClassRepository.GetAllClasses().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } @class => Ok(@class),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting class by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new class
    /// </summary>
    /// <param name="class">Class object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Class
    ///     {
    ///         "Id": "string",
    ///         "StartDate": "dd/MM/yyyy",
    ///         "EndDate": "dd/MM/yyyy",
    ///         "Fee": "float",
    ///         "CourseId": "string",
    ///         "BranchId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new class successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Class id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult InsertClass([FromBody] Class @class)
    {
        try
        {
            var validationResult = _validator.Validate(@class);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (_unitOfWork.ClassRepository.TryGetClassById(@class.Id, out _))
                return Conflict();

            var newClass = _mapper.Map<Domain.Entities.Class>(@class);
            _transactionService.ExecuteTransaction(() => _unitOfWork.ClassRepository.AddClass(newClass));

            var classes =
                _redisCacheService.GetOrSet(CacheKey, () => _unitOfWork.ClassRepository.GetAllClasses().ToList());
            if (classes.FirstOrDefault(s => s.Id == newClass.Id) is null)
                classes.Add(_mapper.Map<Domain.Entities.Class>(newClass));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding class");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete class by id
    /// </summary>
    /// <param name="id">Class id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Class/{id}
    /// </remarks>
    /// <response code="200">Delete class successfully</response>
    /// <response code="404">If no staff is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteClass([FromRoute] string id)
    {
        try
        {
            if (!_unitOfWork.ClassRepository.TryGetClassById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.ClassRepository.DeleteClass(id));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ClassRepository.GetAllClasses().ToList())
                is { Count: > 0 } classes)
                classes.RemoveAll(s => s.Id == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting class");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update class
    /// </summary>
    /// <param name="class">Class object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Class
    ///     {
    ///         "Id": "string",
    ///         "StartDate": "dd/MM/yyyy",
    ///         "EndDate": "dd/MM/yyyy",
    ///         "Fee": "float",
    ///         "CourseId": "string",
    ///         "BranchId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update class successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateClass([FromBody] Class @class)
    {
        try
        {
            var validationResult = _validator.Validate(@class);
            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (!_unitOfWork.ClassRepository.TryGetClassById(@class.Id, out _))
                return NotFound();

            var updateClass = _mapper.Map<Domain.Entities.Class>(@class);
            _transactionService.ExecuteTransaction(() => _unitOfWork.ClassRepository.UpdateClass(updateClass));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ClassRepository.GetAllClasses().ToList())
                is { Count: > 0 } classes)
                classes[classes.FindIndex(s => s.Id == updateClass.Id)] = updateClass;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating class");
            return StatusCode(500);
        }
    }
}