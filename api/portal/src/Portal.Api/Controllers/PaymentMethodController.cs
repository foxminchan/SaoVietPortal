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
public class PaymentMethodController : ControllerBase
{
    private const string CacheKey = "PaymentMethodData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<PaymentMethodController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<PaymentMethod> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public PaymentMethodController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<PaymentMethodController> logger,
        IMapper mapper,
        IValidator<PaymentMethod> validator,
        IRedisCacheService redisCacheService)
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
    }

    /// <summary>
    /// Get all payment methods
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/PaymentMethod
    /// </remarks>
    /// <response code="200">Response the list of payment methods</response>
    /// <response code="404">If no payment methods are found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<PaymentMethod>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetPaymentMethod()
    {
        try
        {
            return (_redisCacheService.GetOrSet(CacheKey,
                    () => _unitOfWork.PaymentMethodRepository.GetAllPaymentMethods().ToList())) switch
            {
                { Count: > 0 } paymentMethods => Ok(_mapper.Map<List<PaymentMethod>>(paymentMethods)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting payment methods");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get payment method by id
    /// </summary>
    /// <param name="id">Payment method id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/PaymentMethod/{id}
    /// </remarks>
    /// <response code="200">Response the payment method</response>
    /// <response code="404">If no payment method is found</response>
    [HttpGet("{id:int}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(PaymentMethod))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetPaymentMethodById([FromRoute] int id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.PaymentMethodRepository
                        .GetAllPaymentMethods().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } paymentMethod => Ok(paymentMethod),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting payment method by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new payment method
    /// </summary>
    /// <param name="paymentMethod">Payment Method object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/PaymentMethod
    ///     {
    ///         "Name": "string"
    ///     }
    /// </remarks>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult InsertPaymentMethod([FromBody] PaymentMethod paymentMethod)
    {
        try
        {
            if (paymentMethod.Id.HasValue)
                return BadRequest("Payment method id is auto generated");

            var validationResult = _validator.Validate(paymentMethod);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            var newPaymentMethod = _mapper.Map<Domain.Entities.PaymentMethod>(paymentMethod);

            _transactionService.ExecuteTransaction(() => _unitOfWork.PaymentMethodRepository.AddPaymentMethod(newPaymentMethod));

            var paymentMethods =
                _redisCacheService.GetOrSet(CacheKey, () => _unitOfWork.PaymentMethodRepository
                    .GetAllPaymentMethods().ToList());
            if (paymentMethods.FirstOrDefault(s => s.Id == newPaymentMethod.Id) is null)
                paymentMethods.Add(_mapper.Map<Domain.Entities.PaymentMethod>(newPaymentMethod));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding payment method");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete payment method by id
    /// </summary>
    /// <param name="id">PaymentMethod id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/PaymentMethod/{id}
    /// </remarks>
    /// <response code="200">Delete payment method successfully</response>
    /// <response code="404">If no payment method is found</response>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeletePaymentMethod([FromRoute] int id)
    {
        try
        {
            if (!_unitOfWork.PaymentMethodRepository.TryGetPaymentMethod(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.PaymentMethodRepository.DeletePaymentMethod(id));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.PaymentMethodRepository.GetAllPaymentMethods().ToList())
                is { Count: > 0 } paymentMethods)
                paymentMethods.RemoveAll(s => s.Id == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting payment method");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update payment method
    /// </summary>
    /// <param name="paymentMethod">Payment method object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/PaymentMethod
    ///     {
    ///         "Id": "int",
    ///         "Name": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update payment method successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If payment method id is not found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdatePaymentMethod([FromBody] PaymentMethod paymentMethod)
    {
        try
        {
            var validationResult = _validator.Validate(paymentMethod);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (paymentMethod.Id.HasValue && !_unitOfWork.PaymentMethodRepository.TryGetPaymentMethod(paymentMethod.Id.Value, out _))
                return NotFound();

            var updatePaymentMethod = _mapper.Map<Domain.Entities.PaymentMethod>(paymentMethod);
            _transactionService.ExecuteTransaction(() => _unitOfWork.PaymentMethodRepository
                .UpdatePaymentMethod(updatePaymentMethod));

            if (_redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.PaymentMethodRepository.GetAllPaymentMethods().ToList())
                is { Count: > 0 } paymentMethods)
                paymentMethods[paymentMethods.FindIndex(s => s.Id == updatePaymentMethod.Id)] = updatePaymentMethod;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating payment method");
            return StatusCode(500);
        }
    }
}