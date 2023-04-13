using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Models;
using Portal.Application.Cache;
using Portal.Application.Services;
using Portal.Application.Transaction;

namespace Portal.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
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
}