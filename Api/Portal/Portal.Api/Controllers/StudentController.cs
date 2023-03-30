using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Portal.Api.Models;
using Portal.Application.Services;
using Portal.Application.Utils;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    private readonly TransactionService _transactionService;
    private readonly ILogger<StudentController> _logger;
    private readonly IMapper _mapper;

    public StudentController(
        StudentService studentService, 
        TransactionService transactionService, 
        ILogger<StudentController> logger, 
        IMapper mapper)
    {
        _studentService = studentService;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Lấy danh sách học viên
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Student
    /// </remarks>
    /// <respone code="200">Trả về danh sách học viên</respone>
    /// <respone code="401">Không có quyền</respone>
    /// <respone code="404">Không tìm thấy học viên</respone>
    /// <respone code="429">Quá nhiều yêu cầu</respone>
    /// <respone code="500">Lỗi server</respone>
    [HttpGet]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(List<Student>))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
    public ActionResult GetStudents()
    {
        try
        {
            return _studentService.GetAllStudents().ToList() switch
            {
                { Count: > 0 } students => Ok(students),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting students");
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Tìm thông tin học viên theo id
    /// </summary>
    /// <returns></returns>
    /// <param name="id">Mã học viên</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /api/v1/Student
    /// </remarks>
    /// <respone code="200">Trả về thông tin học viên</respone>
    /// <respone code="401">Không có quyền</respone>
    /// <respone code="404">Không tìm thấy học viên</respone>
    /// <respone code="429">Quá nhiều yêu cầu</respone>
    /// <respone code="500">Lỗi server</respone>
    [HttpGet("{id}")]
    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(200, Type = typeof(Student))]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [ProducesResponseType(429)]
    [ProducesResponseType(500)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public ActionResult GetStudentById(
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)] 
        [FromRoute] string id)
    {
        try
        {
            return _studentService.GetStudentById(id) switch
            {
                { } student => Ok(student),
                _ => NotFound()
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting student by id");
            return StatusCode(500);
        }
    }
}