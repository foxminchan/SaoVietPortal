﻿using AutoMapper;
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
public class StaffController : ControllerBase
{
    private const string CacheKey = "StaffData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<StaffController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<StaffResponse> _validator;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService<StaffResponse> _luceneService;

    public StaffController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<StaffController> logger,
        IMapper mapper,
        IValidator<StaffResponse> validator,
        IRedisCacheService redisCacheService,
        ILuceneService<StaffResponse> luceneService)
    => (_unitOfWork, _transactionService, _logger, _mapper, _validator, _redisCacheService, _luceneService) =
        (unitOfWork, transactionService, logger, mapper, validator, redisCacheService, luceneService);

    /// <summary>
    /// Get all staffs
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Staff
    /// </remarks>
    /// <response code="200">Returns all staffs</response>
    /// <response code="404">If no staffs found</response>
    [HttpGet]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(typeof(List<StaffResponse>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStaffs()
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                        .GetStaff().ToList()) switch
            {
                { Count: > 0 } staffs => Ok(_mapper.Map<List<StaffResponse>>(staffs)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting staffs");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find staff by id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Staff id</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Staff/{id}
    /// </remarks>
    /// <response code="200">Response the staff</response>
    /// <response code="404">If no staff is found</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200, Type = typeof(StaffResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStaffById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                        .GetStaff().ToList())
                    .FirstOrDefault(s => s.Id == id) switch
            {
                { } staff => Ok(_mapper.Map<StaffResponse>(staff)),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting staff by id");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Find staff by name
    /// </summary>
    /// <param name="name">Staff name</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Staff?name={name}
    /// </remarks>
    /// <response code="200">Response the list of staffs</response>
    /// <response code="404">If no staffs are found</response>
    [HttpGet("search")]
    [ProducesResponseType(200, Type = typeof(StaffResponse))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public IActionResult GetStaffByName([FromQuery(Name = "name"), BindRequired] string name)
    {
        try
        {
            var staffs = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                    .GetStaff().ToList())
                .Select(_mapper.Map<StaffResponse>).ToList();

            if (!staffs.Any()) return NotFound();

            if (!_luceneService.IsExistIndex(staffs.First()))
                _luceneService.Index(staffs, nameof(LuceneOptions.Create));

            return _luceneService.Search(name, 20).ToList() switch
            {
                { Count: > 0 } result => Ok(result),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting staff by name");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Add new staff
    /// </summary>
    /// <param name="staff">Staff object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/Staff
    ///     {
    ///         "StudentId": "string",
    ///         "Fullname": "string",
    ///         "Dob": "dd/MM/yyyy",
    ///         "Address": "string",
    ///         "Dsw": "dd/MM/yyyy",
    ///         "PositionId": integer,
    ///         "BranchId": "string",
    ///         "ManagerId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new staff successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Staff id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public IActionResult InsertStaff([FromBody] StaffResponse staff)
    {
        try
        {
            var validationResult = _validator.Validate(staff);

            if (!validationResult.IsValid)
            {
                _logger.LogError("Validation errors: {@Errors}", validationResult.Errors);
                return BadRequest(new ValidationError(validationResult));
            }

            if (_unitOfWork.StaffRepository.TryGetStaffById(staff.Id, out _))
                return Conflict();

            var newStaff = _mapper.Map<Staff>(staff);

            _transactionService.ExecuteTransaction(() => _unitOfWork.StaffRepository.AddStaff(newStaff));

            var staffs = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                    .GetStaff().ToList());

            if (staffs.FirstOrDefault(s => s.Id == newStaff.Id) is null)
                staffs.Add(_mapper.Map<Staff>(newStaff));

            _luceneService
                .Index(staffs
                    .Select(_mapper.Map<StaffResponse>).ToList(), nameof(LuceneOptions.Create));

            _logger.LogInformation("Completed request {@RequestName}", nameof(InsertStaff));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{newStaff.Id}"), newStaff);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while adding staff");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Delete staff by id
    /// </summary>
    /// <param name="id">Staff id</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /api/v1/Staff/{id}
    /// </remarks>
    /// <response code="200">Delete staff successfully</response>
    /// <response code="404">If no staff is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult DeleteStaff([FromRoute] string id)
    {
        try
        {
            if (!_unitOfWork.StaffRepository.TryGetStaffById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _unitOfWork.StaffRepository.DeleteStaff(id));

            var staffs = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                    .GetStaff().ToList());

            if (staffs.FirstOrDefault(s => s.Id == id) is { } staff)
                staffs.Remove(staff);

            _luceneService
                .Index(staffs
                    .Select(_mapper.Map<StaffResponse>).ToList(), nameof(LuceneOptions.Delete));

            _logger.LogInformation("Completed request {@RequestName}", nameof(DeleteStaff));

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while deleting staff");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Update staff
    /// </summary>
    /// <param name="staff">Staff object</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/v1/Staff
    ///     {
    ///         "StudentId": "string",
    ///         "Fullname": "string",
    ///         "Dob": "dd/MM/yyyy",
    ///         "Address": "string",
    ///         "Dsw": "dd/MM/yyyy",
    ///         "PositionId": integer,
    ///         "BranchId": "string",
    ///         "ManagerId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update staff successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult UpdateStaff([FromBody] StaffResponse staff)
    {
        try
        {
            var validationResult = _validator.Validate(staff);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (!_unitOfWork.StaffRepository.TryGetStaffById(staff.Id, out _))
                return NotFound();

            var updateStaff = _mapper.Map<Staff>(staff);
            _transactionService.ExecuteTransaction(() => _unitOfWork.StaffRepository.UpdateStaff(updateStaff));

            var staffs = _redisCacheService
                .GetOrSet(CacheKey, () => _unitOfWork.StaffRepository
                    .GetStaff().ToList());

            if (staffs.FirstOrDefault(s => s.Id == updateStaff.Id) is { } data)
                staffs[staffs.IndexOf(data)] = updateStaff;

            _luceneService
                .Index(staffs
                    .Select(_mapper.Map<StaffResponse>).ToList(), nameof(LuceneOptions.Update));

            _logger.LogInformation("Completed request {@RequestName}", nameof(UpdateStaff));

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{updateStaff.Id}"), updateStaff);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating staff");
            return StatusCode(500);
        }
    }
}