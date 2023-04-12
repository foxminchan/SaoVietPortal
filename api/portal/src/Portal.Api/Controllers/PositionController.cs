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
public class PositionController : ControllerBase
{
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


}