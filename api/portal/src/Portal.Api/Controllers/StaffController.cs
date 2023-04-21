using AutoMapper;
using FluentValidation;
using Lucene.Net.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Search;
using Portal.Application.Services;
using Portal.Application.Transaction;
using Portal.Domain.Primitives;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class StaffController : ControllerBase
{
    private const string CACHE_KEY = "StaffData";
    private readonly StaffService _staffService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<StaffController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<Staff> _validator;
    private readonly IRedisCacheService _redisCacheService;
    private readonly ILuceneService _luceneService;

    public StaffController(
        StaffService staffService,
        TransactionService transactionService,
        ILogger<StaffController> logger,
        IMapper mapper,
        IValidator<Staff> validator,
        IRedisCacheService redisCacheService,
        ILuceneService luceneService
    )
    {
        _staffService = staffService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
        _luceneService = luceneService;
    }

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
    [ProducesResponseType(typeof(List<Staff>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStaffs()
    {
        try
        {
            return _redisCacheService.GetOrSet(CACHE_KEY,
                    () => _staffService.GetStaff().ToList()) switch
            {
                { Count: > 0 } staffs => Ok(_mapper.Map<List<Staff>>(staffs)),
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
    [ProducesResponseType(200, Type = typeof(Staff))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStaffById([FromRoute] string id)
    {
        try
        {
            return _redisCacheService
                    .GetOrSet(CACHE_KEY, () => _staffService.GetStaff().ToList())
                    .FirstOrDefault(s => s.staffId == id) switch
            {
                { } staff => Ok(staff),
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
    [ProducesResponseType(200, Type = typeof(Staff))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    [ResponseCache(Duration = 15)]
    public ActionResult GetStaffByName([FromQuery(Name = "name"), BindRequired] string name)
    {
        try
        {
            var staffs = _redisCacheService
                .GetOrSet(CACHE_KEY, () => _staffService.GetStaff().ToList())
                .Select(_mapper.Map<Staff>).ToList();

            if (!staffs.Any()) return NotFound();

            var propertyIndex = new Dictionary<string, List<Document>>();

            foreach (var staff in staffs)
            {
                foreach (var property in staff.GetType().GetProperties())
                {
                    if (!propertyIndex.ContainsKey(property.Name))
                        propertyIndex.Add(property.Name, new List<Document>());

                    var value = property.GetValue(staff, null);

                    if (value is null) continue;

                    var document = new Document
                        { new StringField(property.Name, value.ToString(), Field.Store.YES) };

                    propertyIndex[property.Name].Add(document);
                }
            }

            _luceneService.Index(propertyIndex);

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
    ///         "staffId": "string",
    ///         "fullname": "string",
    ///         "dob": "dd/MM/yyyy",
    ///         "address": "string",
    ///         "dsw": "dd/MM/yyyy",
    ///         "positionId": integer,
    ///         "branchId": "string",
    ///         "managerId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Add new staff successfully</response>
    /// <response code="400">Invalid input</response>
    /// <response code="409">Staff id has already existed</response>
    [HttpPost]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public ActionResult InsertStaff([FromBody] Staff staff)
    {
        try
        {
            var validationResult = _validator.Validate(staff);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (staff.staffId is not null && _staffService.TryGetStaffById(staff.staffId, out _))
                return Conflict();

            var newStaff = _mapper.Map<Domain.Entities.Staff>(staff);

            _transactionService.ExecuteTransaction(() => _staffService.AddStaff(newStaff));

            var staffs = 
                _redisCacheService.GetOrSet(CACHE_KEY, () => _staffService.GetStaff().ToList());
            if (staffs.FirstOrDefault(s => s.staffId == newStaff.staffId) is null)
                staffs.Add(_mapper.Map<Domain.Entities.Staff>(newStaff));

            return Ok();
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
    /// <response code="400">The input is invalid</response>
    /// <response code="404">If no staff is found</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult DeleteStaff([FromRoute] string id)
    {
        try
        {
            if (!_staffService.TryGetStaffById(id, out _))
                return NotFound();

            _transactionService.ExecuteTransaction(() => _staffService.DeleteStaff(id));

            if (_redisCacheService
                    .GetOrSet(CACHE_KEY, () => _staffService.GetStaff().ToList()) 
                is { Count: > 0 } staffs)
                staffs.RemoveAll(s => s.staffId == id);

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
    ///         "staffId": "string",
    ///         "fullname": "string",
    ///         "dob": "dd/MM/yyyy",
    ///         "address": "string",
    ///         "dsw": "dd/MM/yyyy",
    ///         "positionId": integer,
    ///         "branchId": "string",
    ///         "managerId": "string"
    ///     }
    /// </remarks>
    /// <response code="200">Update staff successfully</response>
    /// <response code="400">Invalid input</response>
    [HttpPut]
    [Authorize(Policy = "Developer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(ValidationError))]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult UpdateStaff([FromBody] Staff staff)
    {
        try
        {
            var validationResult = _validator.Validate(staff);

            if (!validationResult.IsValid)
                return BadRequest(new ValidationError(validationResult));

            if (staff.staffId is not null && !_staffService.TryGetStaffById(staff.staffId, out _))
                return NotFound();

            var updateStaff = _mapper.Map<Domain.Entities.Staff>(staff);
            _transactionService.ExecuteTransaction(() => _staffService.UpdateStaff(updateStaff));

            if (_redisCacheService
                    .GetOrSet(CACHE_KEY, () => _staffService.GetStaff().ToList()) 
                is { Count: > 0 } staffs)
                staffs[staffs.FindIndex(s => s.staffId == updateStaff.staffId)] = updateStaff;

            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while updating staff");
            return StatusCode(500);
        }
    }
}