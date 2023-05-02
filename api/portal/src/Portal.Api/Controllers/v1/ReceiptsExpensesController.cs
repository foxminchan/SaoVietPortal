using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Entities;
using Portal.Domain.Interfaces.Common;
using Portal.Domain.Options;

namespace Portal.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class ReceiptsExpensesController : ControllerBase
{
    private const string CacheKey = "ReceiptsExpensesData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<ReceiptsExpensesController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<ReceiptsExpensesResponse> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public ReceiptsExpensesController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<ReceiptsExpensesController> logger,
        IMapper mapper,
        IValidator<ReceiptsExpensesResponse> validator,
        IRedisCacheService redisCacheService)
        => (_unitOfWork, _transactionService, _logger, _mapper, _validator, _redisCacheService) =
            (unitOfWork, transactionService, logger, mapper, validator, redisCacheService);

    /// <summary>
    /// Get all receipts and expenses
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/ReceiptsExpenses
    /// </remarks>
    /// <response code="200">Response the list of receipts and expenses</response>
    /// <response code="404">If no receipts and expenses found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<ReceiptsExpensesResponse>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetReceiptsExpenses()
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ReceiptsExpensesRepository
                        .GetReceiptsExpenses().ToList()) switch
            {
                { Count: > 0 } receiptsExpenses => Ok(_mapper
                    .Map<List<ReceiptsExpensesResponse>>(receiptsExpenses)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting receipts and expenses");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get receipts/expenses by id
    /// </summary>
    /// <param name="id">Receipts/expenses id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/ReceiptsExpenses/{id}
    /// </remarks>
    /// <response code="200">Response the receipts/expenses</response>
    /// <response code="404">If no receipts/expenses found</response>
    [HttpGet("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(ReceiptsExpensesResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetReceiptsExpensesById([FromRoute] Guid id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ReceiptsExpensesRepository
                        .GetReceiptsExpenses().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } receiptsExpenses => Ok(_mapper.Map<ReceiptsExpensesResponse>(receiptsExpenses)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting receipts/expenses by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new receipts/expenses
    /// </summary>
    /// <param name="receiptsExpenses">Receipts/expenses object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/ReceiptsExpenses
    ///     {
    ///         "Type": bool,
    ///         "Date": "dd/MM/yyyy",
    ///         "Amount": float,
    ///         "Note": "string",
    ///         "BranchId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new receipts/expenses successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="409">Receipts/expenses id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    [ProducesDefaultResponseType]
    public IActionResult InsertReceiptsExpenses([FromBody] ReceiptsExpensesResponse receiptsExpenses)
    {
        try
        {
            var validationResult = _validator.Validate(receiptsExpenses);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (_unitOfWork.ReceiptsExpensesRepository.TryGetReceiptsExpenses(receiptsExpenses.Id, out _))
                return Conflict();

            var newReceiptsExpenses = _mapper.Map<ReceiptsExpenses>(receiptsExpenses);

            _transactionService.ExecuteTransaction(
                () => _unitOfWork.ReceiptsExpensesRepository.AddReceiptsExpenses(newReceiptsExpenses));

            var receiptsExpense = _redisCacheService
                .GetOrSet(CacheKey,
                    () => _unitOfWork.ReceiptsExpensesRepository.GetReceiptsExpenses().ToList());

            if (receiptsExpense.FirstOrDefault(s => s.Id == newReceiptsExpenses.Id) is null)
                receiptsExpense.Add(_mapper.Map<ReceiptsExpenses>(newReceiptsExpenses));

            _logger.LogInformation("Completed request {@RequestName}", nameof(InsertReceiptsExpenses));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{newReceiptsExpenses.Id}"), newReceiptsExpenses);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding receipts/expenses");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete receipts/expenses by id
    /// </summary>
    /// <param name="id">Receipts/expenses id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/ReceiptsExpenses/{id}
    /// </remarks>
    /// <response code="200">Delete receipts/expenses successfully</response>
    /// <response code="404">If no receipts/expenses found</response>
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteReceiptsExpenses([FromRoute] Guid id)
    {
        try
        {
            if (_unitOfWork.ReceiptsExpensesRepository.TryGetReceiptsExpenses(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(
                () => _unitOfWork.ReceiptsExpensesRepository.DeleteReceiptsExpenses(id));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ReceiptsExpensesRepository
                        .GetReceiptsExpenses().ToList()) is { Count: > 0 } receiptsExpenses)
                receiptsExpenses.RemoveAll(s => s.Id == id);

            _logger.LogInformation("Completed request {@RequestName}", nameof(DeleteReceiptsExpenses));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting receipts/expenses");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update receipts/expenses
    /// </summary>
    /// <param name="receiptsExpenses">Receipts/expenses object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/ReceiptsExpenses
    ///     {
    ///         "Id": "Guid",
    ///         "Type": bool,
    ///         "Date": "dd/MM/yyyy",
    ///         "Amount": float,
    ///         "Note": "string",
    ///         "BranchId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update receipts/expenses successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no receipts/expenses found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateReceiptsExpenses([FromBody] ReceiptsExpensesResponse receiptsExpenses)
    {
        try
        {
            var validationResult = _validator.Validate(receiptsExpenses);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (!_unitOfWork.ReceiptsExpensesRepository.TryGetReceiptsExpenses(receiptsExpenses.Id, out _))
                return NotFound();

            var updateReceiptsExpenses = _mapper.Map<ReceiptsExpenses>(receiptsExpenses);
            _transactionService.ExecuteTransaction(
                () => _unitOfWork.ReceiptsExpensesRepository.UpdateReceiptsExpenses(updateReceiptsExpenses));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.ReceiptsExpensesRepository
                        .GetReceiptsExpenses().ToList()) is
                { Count: > 0 } receiptsExpense)
                receiptsExpense[receiptsExpense
                        .FindIndex(s => s.Id == updateReceiptsExpenses.Id)] = updateReceiptsExpenses;

            _logger.LogInformation("Completed request {@RequestName}", nameof(UpdateReceiptsExpenses));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{updateReceiptsExpenses.Id}"), updateReceiptsExpenses);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating receipts/expenses");
            return StatusCode(500);
        }
    }
}