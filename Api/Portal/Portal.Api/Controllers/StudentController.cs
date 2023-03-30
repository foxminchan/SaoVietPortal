using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}