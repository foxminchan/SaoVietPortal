﻿namespace SaoViet.Portal.Application.DTOs;

public record ReceiptsExpensesDto(Guid Id, bool Type, string Date, float Amount, string Note, string BranchId);