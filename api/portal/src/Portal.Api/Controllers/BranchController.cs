using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Services;
using Portal.Application.Transaction;
using System.Net.Mime;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class BranchController : ControllerBase
{
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
    /// <response code="404">No branch found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Branch>))]
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
    public ActionResult GetBranches()
    {
        try
        {
            return (_redisCacheService.GetOrSet("BranchData",
                    () => _branchService.GetAllBranches().ToList())) switch
            {
                { Count: > 0 } branches => Ok(branches),
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
    /// <response code="404">No branch found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Branch))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    [ProducesResponseType(406)]
    [ProducesResponseType(408)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [ResponseCache(Duration = 15)]
    public ActionResult GetBranchById(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet("BranchData", () => _branchService.GetAllBranches().ToList())
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
    ///         "branchId": "TMBH0001",
    ///         "branchName": "Tân Mai Biên Hoà",
    ///         "address": "Số 46B/3, KP 2, Phường Tân Mai, Tp Biên Hòa, Đồng Nai",
    ///         "phone": "0931144858"
    ///     }
    /// </remarks>
    /// <response code="200">Add new branch successfully</response>
    /// <response code="404">No branch found</response>
    /// <response code="409">Branch id has already existed</response>
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
    public ActionResult InsertPosition(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        [FromBody] Branch branch)
    {
        try
        {
            if (_validator.Validate(branch).IsValid)
                return BadRequest();

            if (branch.branchId != null && _branchService.GetBranchById(branch.branchId) != null)
                return Conflict();

            var newBranch = _mapper.Map<Domain.Entities.Branch>(branch);

            _transactionService.ExecuteTransaction(() => _branchService.AddBranch(newBranch));

            var branches = _redisCacheService.GetOrSet("BranchData", () => _branchService.GetAllBranches().ToList());
            if (branches.FirstOrDefault(s => s.branchId == newBranch.branchId) == null)
                branches.Add(_mapper.Map<Domain.Entities.Branch>(newBranch));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding branch");
            return StatusCode(500);
        }
    }
}