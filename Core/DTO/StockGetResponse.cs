namespace Core.DTO;

public record StockGetResponse(int Id, string Symbol, string CompanyName, decimal Price, decimal DividendYield, string Industry, long MarketCap, List<CommentGetResponse> Comments);