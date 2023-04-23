using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Transaction;
using Portal.Domain.Interfaces.Common;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CourseRegistrationController : ControllerBase
{
    private const string CacheKey = "CourseRegistrationData";
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<CourseRegistrationController> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<CourseRegistration> _validator;
    private readonly IRedisCacheService _redisCacheService;

    public CourseRegistrationController(
        IUnitOfWork unitOfWork,
        ITransactionService transactionService,
        ILogger<CourseRegistrationController> logger,
        IMapper mapper,
        IValidator<CourseRegistration> validator,
        IRedisCacheService redisCacheService)
    {
        _unitOfWork = unitOfWork;
        _transactionService = transactionService;
        _logger = logger;
        _mapper = mapper;
        _validator = validator;
        _redisCacheService = redisCacheService;
    }
}