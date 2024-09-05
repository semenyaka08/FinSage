using System.Linq.Expressions;
using Core.Domain.Models;
using Core.Domain.RepositoryContracts;
using Core.DTO;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;

    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllStocksAsync(StockGetRequest stockGetRequest)
    {
        var query = _context.Stocks.AsQueryable();

        if (stockGetRequest.SearchString != null)
            query = query
                .Where(z => z.CompanyName.Contains(stockGetRequest.SearchString) ||
                            z.Symbol.Contains(stockGetRequest.SearchString) ||
                            z.Industry.Contains(stockGetRequest.SearchString));

        query = stockGetRequest.SortOrder == "desc" ? query.OrderByDescending(GetKey(stockGetRequest.SortItem)) : query.OrderBy(GetKey(stockGetRequest.SortItem));

        var skipNumber = (stockGetRequest.PageNumber - 1) * stockGetRequest.PageSize;

        query = query.Skip(skipNumber).Take(stockGetRequest.PageSize);
        
        return await query.Include(z=>z.Comments)
            .ThenInclude(z=>z.User)
            .ToListAsync();
    }

    private Expression<Func<Stock, object>> GetKey(string? sortItem)
    {
        switch (sortItem)
        {
            case "price":
                return stock => stock.Price;
            case "div":
                return stock => stock.DividendYield;
            case "cap":
                return stock => stock.MarketCap;
            default:
                return stock => stock.Id; 
        }
    }
    
    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(z=>z.Comments).FirstOrDefaultAsync(k=>k.Id == id);
    }

    public async Task AddStockAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
    }

    public async Task<Stock?> UpdateStockAsync(int id, Stock updatedStock)
    {
        var stock = await GetByIdAsync(id);

        if (stock == null)
            return stock;

        stock.Industry = updatedStock.Industry;
        stock.CompanyName = updatedStock.CompanyName;
        stock.Price = updatedStock.Price;
        stock.Symbol = updatedStock.Symbol;
        stock.DividendYield = updatedStock.DividendYield;
        stock.MarketCap = updatedStock.MarketCap;

        await _context.SaveChangesAsync();
        
        return stock;
    }

    public async Task<bool> DeleteStock(int id)
    {
        var stock = await GetByIdAsync(id);

        if (stock == null)
            return false;

        _context.Stocks.Remove(stock);

        await _context.SaveChangesAsync();
        
        return true;
    }
}