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
public class BranchController : ControllerBase
{
    private const string CacheKey = "BranchData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<BranchController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Branch> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public BranchController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<BranchController> logger,
        IMapper mapper,
        IValidator<Branch> validator,
        IRedisCacheService redisCacheService)
    => (_unitOfWork, _transactionService, _logger, _mapper, _validator, _redisCacheService) =
        (unitOfWork, transactionService, logger, mapper, validator, redisCacheService);

    /// <summary>
    /// Get all branches
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Branch
    /// </remarks>
    /// <response code="200">Response the list of branches</response>
    /// <response code="404">If no branches found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Branch>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetBranches()
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.BranchRepository
                        .GetAllBranches().ToList()) switch
            {
                { Count: > 0 } branches => Ok(_mapper.Map<List<Branch>>(branches)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting branches");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get branch by id
    /// </summary>
    /// <param name="id">Branch id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Branch/{id}
    /// </remarks>
    /// <response code="200">Response the branch</response>
    /// <response code="404">If no branch found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Branch))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetBranchById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.BranchRepository
                        .GetAllBranches().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } branch => Ok(branch),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting branch by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new branch
    /// </summary>
    /// <param name="branch">Branch object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Branch
    ///     {
    ///         "Id": "string",
    ///         "Name": "string",
    ///         "Address": "string",
    ///         "Phone": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new branch successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="409">Branch id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    [ProducesDefaultResponseType]
    public IActionResult InsertBranch([FromBody] Branch branch)
    {
        try
        {
            var validationResult = _validator.Validate(branch);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (_unitOfWork.BranchRepository.TryGetBranchById(branch.Id, out _))
                return Conflict();

            var newBranch = _mapper.Map<Domain.Entities.Branch>(branch);

            _transactionService.ExecuteTransaction(() => _unitOfWork.BranchRepository.AddBranch(newBranch));

            var branches = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.BranchRepository
                    .GetAllBranches().ToList());

            if (branches.FirstOrDefault(s => s.Id == newBranch.Id) is null)
                branches.Add(_mapper.Map<Domain.Entities.Branch>(newBranch));

            _logger.LogInformation("Completed request {@RequestName}", nameof(InsertBranch));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{newBranch.Id}"), newBranch);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding branch");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete branch by id
    /// </summary>
    /// <param name="id">Branch id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Branch/{id}
    /// </remarks>
    /// <response code="200">Delete branch successfully</response>
    /// <response code="404">If no branch found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteBranch([FromRoute] string id)
    {
        try
        {
            if (_unitOfWork.BranchRepository.TryGetBranchById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.BranchRepository.DeleteBranch(id));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.BranchRepository
                        .GetAllBranches().ToList()) is { Count: > 0 } branches)
                branches.RemoveAll(s => s.Id == id);

            _logger.LogInformation("Completed request {@RequestName}", nameof(DeleteBranch));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting branch");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="branch">Branch object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Branch
    ///     {
    ///         "Id": "string",
    ///         "Name": "string",
    ///         "Address": "string",
    ///         "Phone": "string"
    ///     }
    /// </remarks>
    /// <response code="200">update branch successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no branch found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdatePosition([FromBody] Branch branch)
    {
        try
        {
            var validationResult = _validator.Validate(branch);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (!_unitOfWork.BranchRepository.TryGetBranchById(branch.Id, out _))
                return NotFound();

            var updateBranch = _mapper.Map<Domain.Entities.Branch>(branch);
            _transactionService.ExecuteTransaction(() => _unitOfWork.BranchRepository.UpdateBranch(updateBranch));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.BranchRepository
                        .GetAllBranches().ToList()) is
                { Count: > 0 } branches)
                branches[branches.FindIndex(s => s.Id == updateBranch.Id)] = updateBranch;

            _logger.LogInformation("Completed request {@RequestName}", nameof(UpdatePosition));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{updateBranch.Id}"), updateBranch);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating position");
            return StatusCode(500);
        }
    }
}