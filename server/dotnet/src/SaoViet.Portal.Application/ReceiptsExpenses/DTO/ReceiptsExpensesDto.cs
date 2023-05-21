namespace SaoViet.Portal.Application.ReceiptsExpenses.DTO;

public record ReceiptsExpensesDto(Guid Id, bool Type, string Date, float Amount, string Note, string BranchId);