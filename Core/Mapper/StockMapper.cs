using Core.Domain.Models;
using Core.DTO;

namespace Core.Mapper;

public static class StockMapper
{
    public static StockGetResponse ToStockGetResponse(this Stock stock)
    {
        return new StockGetResponse
        (
            stock.Id,
            stock.Symbol,
            stock.CompanyName,
            stock.Price,
            stock.DividendYield,
            stock.Industry,
            stock.MarketCap,
            stock.Comments.Select(z=>z.ToCommentGetResponse()).ToList()
        );
    }
    
    public static Stock ToStock(this BaseStockRequest stock)
    {
        return new Stock
        {
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Price = stock.Price,
            DividendYield = stock.DividendYield,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
}