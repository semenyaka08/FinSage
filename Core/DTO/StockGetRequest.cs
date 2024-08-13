namespace Core.DTO;

public record StockGetRequest(string? SearchString, string? SortItem, string? SortOrder, int PageNumber = 1, int PageSize = 5);