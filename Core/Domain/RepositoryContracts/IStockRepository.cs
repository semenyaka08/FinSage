using Core.Domain.Models;
using Core.DTO;

namespace Core.Domain.RepositoryContracts;

public interface IStockRepository
{
    public Task<List<Stock>> GetAllStocksAsync(StockGetRequest stockGetRequest);

    public Task<Stock?> GetByIdAsync(int id);

    public Task AddStockAsync(Stock stock);

    public Task<Stock?> UpdateStockAsync(int id, Stock stock);

    public Task<bool> DeleteStock(int id);
}