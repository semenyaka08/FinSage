using Core.Domain.RepositoryContracts;
using Core.DTO;
using Core.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockRepository _stockRepository;

    public StockController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllStocks([FromQuery] StockGetRequest stockGetRequest)
    {
        var stocks = await _stockRepository.GetAllStocksAsync(stockGetRequest);

        return Ok(stocks.Select(z=>z.ToStockGetResponse()));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock == null)
        {
            return NotFound();
        }

        return Ok(stock.ToStockGetResponse());
    }

    [HttpPost]
    public async Task<IActionResult> AddStock([FromBody] StockAddRequest addRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = addRequest.ToStock();
        
        await _stockRepository.AddStockAsync(stock);
        
        return CreatedAtAction(nameof(GetStockById), new { id = stock.Id }, stock.ToStockGetResponse());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockUpdateRequest stockUpdateRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = stockUpdateRequest.ToStock();

        var updatedStock = await _stockRepository.UpdateStockAsync(id, stock);

        return Ok(updatedStock);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStock([FromRoute] int id)
    {
        var result = await _stockRepository.DeleteStock(id);

        if (result == false)
            return NotFound();

        return NoContent();
    }
}