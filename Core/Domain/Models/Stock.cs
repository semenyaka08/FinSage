using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Models;

public class Stock
{
    public int Id { get; set; }

    public string Symbol { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal DividendYield { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }

    public ICollection<Comment> Comments { get; set; } = [];
}