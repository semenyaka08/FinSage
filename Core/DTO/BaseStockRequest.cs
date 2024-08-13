using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.DTO;

public abstract class BaseStockRequest
{
    [Required]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    public string CompanyName { get; set; } = string.Empty;
    
    [Range(0, 9999999999.999, ErrorMessage = "Given value is out of range!!")]
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Range(0, 9999999999.999, ErrorMessage = "Given value is out of range!!")]
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal DividendYield { get; set; }
    
    [Required]
    public string Industry { get; set; } = string.Empty;
    
    [Range(0, long.MaxValue, ErrorMessage = "Given value is out of range!!")]
    [Required]
    public long MarketCap { get; set; }
}