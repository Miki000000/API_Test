using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Dtos.StockDTO;
using ApiTest.Models;

namespace ApiTest.Mappers;

public static class StockMappers
{
    public static GetStockRequestDTO ToStockDTO(this Stock stock)
    {
        return new GetStockRequestDTO
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
    public static Stock FromCreateToStock(this CreateStockRequestDTO stock)
    {
        return new Stock
        {
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap
        };
    }
}
