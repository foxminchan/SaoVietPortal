namespace SaoViet.Portal.Infrastructure.CQRS;

public record ListResultModel<T>(List<T> Items, long TotalItems, int Page, int PageSize)
    where T : notnull
{
    public static ListResultModel<T> Create(List<T> items, long totalItems = 0, int page = 1, int pageSize = 30)
    {
        return new ListResultModel<T>(items, totalItems, page, pageSize);
    }
}