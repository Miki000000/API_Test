using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Dtos.CommentDTO;
using ApiTest.Models;

namespace ApiTest.Dtos.StockDTO;

public class GetStockRequestDTO
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
    public List<GetCommentRequestDTO> Comments { get; set; } = new List<GetCommentRequestDTO>();
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}
