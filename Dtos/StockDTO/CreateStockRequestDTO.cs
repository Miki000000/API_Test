using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Dtos.StockDTO;

public class CreateStockRequestDTO
{
    [Required]
    [MaxLength(10, ErrorMessage = "The Symbol must not exceed 10 characters")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(40, ErrorMessage = "The Company Name must not exceed 40 characters")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 10000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(15, ErrorMessage = "The Industry must not exceed 15 characters")]
    public string Industry { get; set; } = string.Empty;
    [Required]
    [Range(1, 10000000000)]
    public long MarketCap { get; set; }
}
