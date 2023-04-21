using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.Primitives;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class BranchController : ControllerBase
{
    private const string CACHE_KEY = "BranchData";
    private readonly BranchService _branchService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<BranchController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Branch> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public BranchController(
        BranchService branchService,
        TransactionService transactionService,
        ILogger<BranchController> logger,
        IMapper mapper,
        IValidator<Branch> validator,
        IRedisCacheService redisCacheService)
    {
        _branchService = branchService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
    }

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
    public ActionResult GetBranches()
    {
        try
        {
            return (_redisCacheService.GetOrSet(CACHE_KEY,
                    () => _branchService.GetAllBranches().ToList())) switch
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
    public ActionResult GetBranchById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CACHE_KEY, () => _branchService.GetAllBranches().ToList())
                    .FirstOrDefault(s => s.branchId == id) switch
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
    ///         "branchId": "string",
    ///         "branchName": "string",
    ///         "address": "string",
    ///         "phone": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new branch successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="409">Branch id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public ActionResult InsertPosition([FromBody] Branch branch)
    {
        try
        {
            var validationResult = _validator.Validate(branch);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (branch.branchId is not null && _branchService.TryGetBranchById(branch.branchId, out _))
                return Conflict();

            var newBranch = _mapper.Map<Domain.Entities.Branch>(branch);

            _transactionService.ExecuteTransaction(() => _branchService.AddBranch(newBranch));

            var branches = _redisCacheService.GetOrSet(CACHE_KEY, () => _branchService.GetAllBranches().ToList());
            if (branches.FirstOrDefault(s => s.branchId == newBranch.branchId) is null)
                branches.Add(_mapper.Map<Domain.Entities.Branch>(newBranch));

            return Ok();
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
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no branch found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult DeleteBranch([FromRoute] string id)
    {
        try
        {
            if (_branchService.TryGetBranchById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _branchService.DeleteBranch(id));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _branchService.GetAllBranches().ToList()) is
                { Count: > 0 } branches)
                branches.RemoveAll(s => s.branchId == id);

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
    ///         "branchId": "string",
    ///         "branchName": "string",
    ///         "address": "string",
    ///         "phone": "string"
    ///     }
    /// </remarks>
    /// <response code="200">update branch successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no branch found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult UpdatePosition([FromBody] Branch branch)
    {
        try
        {
            var validationResult = _validator.Validate(branch);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (branch.branchId is not null && !_branchService.TryGetBranchById(branch.branchId, out _))
                return NotFound();

            var updateBranch = _mapper.Map<Domain.Entities.Branch>(branch);
            _transactionService.ExecuteTransaction(() => _branchService.UpdateBranch(updateBranch));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _branchService.GetAllBranches().ToList()) is
                { Count: > 0 } branches)
                branches[branches.FindIndex(s => s.branchId == updateBranch.branchId)] = updateBranch;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating position");
            return StatusCode(500);
        }
    }
}