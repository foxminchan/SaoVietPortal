using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.ValueObjects;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class PositionController : ControllerBase
{
    private const string CACHE_KEY = "PositionData";
    private readonly PositionService _positionService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<PositionController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Position> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public PositionController(
        PositionService positionService,
        TransactionService transactionService,
        ILogger<PositionController> logger,
        IMapper mapper,
        IValidator<Position> validator,
        IRedisCacheService redisCacheService)
    {
        _positionService = positionService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
    }

    /// <summary>
    /// Get all positions
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Position
    /// </remarks>
    /// <response code="200">Response the list of positions</response>
    /// <response code="404">If no positions are found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(List<Position>))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetPositions()
    {
        try
        {
            return (_redisCacheService.GetOrSet(CACHE_KEY,
                    () => _positionService.GetAllPositions().ToList())) switch
            {
                { Count: > 0 } positions => Ok(_mapper.Map<List<Position>>(positions)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting positions");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Get position by id
    /// </summary>
    /// <param name="id">Position id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Position/{id}
    /// </remarks>
    /// <response code="200">Response the position</response>
    /// <response code="404">If no position is found</response>
    [HttpGet("{id:int}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(Position))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetPositionById([FromRoute] int id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CACHE_KEY, () => _positionService.GetAllPositions().ToList())
                    .FirstOrDefault(s => s.positionId == id) switch
            {
                { } position => Ok(position),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting position by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new position
    /// </summary>
    /// <param name="position">Position object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Position
    ///     {
    ///         "positionId": "int",
    ///         "positionName": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new position successfully</response>
    /// <response code="409">Position id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public ActionResult InsertPosition([FromBody] Position position)
    {
        try
        {

            var validationResult = _validator.Validate(position);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (position.positionId.HasValue)
                return Conflict();

            var newPosition = _mapper.Map<Domain.Entities.Position>(position);

            _transactionService.ExecuteTransaction(() => _positionService.AddPosition(newPosition));

            var positions =
                _redisCacheService.GetOrSet(CACHE_KEY, () => _positionService.GetAllPositions().ToList());
            if (positions.FirstOrDefault(s => s.positionId == newPosition.positionId) == null)
                positions.Add(_mapper.Map<Domain.Entities.Position>(newPosition));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding position");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete position by id
    /// </summary>
    /// <param name="id">Position id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Position/{id}
    /// </remarks>
    /// <response code="200">Delete position successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no position is found</response>
    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult DeletePosition([FromRoute] int id)
    {
        try
        {
            if (_positionService.GetPositionById(id) != null)
                return NotFound();

            _transactionService.ExecuteTransaction(() => _positionService.DeletePosition(id));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _positionService.GetAllPositions().ToList()) is
                { Count: > 0 } positions)
                positions.RemoveAll(s => s.positionId == id);

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting position");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update position
    /// </summary>
    /// <param name="position">Position object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Position
    ///     {
    ///         "id": "int",
    ///         "positionName": "string"
    ///     }
    /// </remarks>
    /// <response code="200">update position successfully</response>
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If position id is not found</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult UpdatePosition([FromBody] Position position)
    {
        try
        {
            var validationResult = _validator.Validate(position);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (_positionService.GetPositionById(position.positionId) == null)
                return NotFound();

            var updatePosition = _mapper.Map<Domain.Entities.Position>(position);
            _transactionService.ExecuteTransaction(() => _positionService.UpdatePosition(updatePosition));

            if (_redisCacheService.GetOrSet(CACHE_KEY, () => _positionService.GetAllPositions().ToList()) is
                { Count: > 0 } positions)
                positions[positions.FindIndex(s => s.positionId == updatePosition.positionId)] = updatePosition;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating position");
            return StatusCode(500);
        }
    }
}