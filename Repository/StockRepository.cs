using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Interfaces;
using ApiTest.Data;
using ApiTest.Dtos.StockDTO;
using ApiTest.Helpers;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace API_A.Repository;

public class StockRepository(ApplicationDBContext context)
: IStockRepository
{
    public async Task<Stock> CreateAsync(CreateStockRequestDTO _stockModel)
    {
        Stock stockModel = _stockModel.FromCreateToStock();
        await context.Stock.AddAsync(stockModel);
        await context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        Stock? stockModel = await context.Stock.FirstOrDefaultAsync(row => row.Id == id);
        if (stockModel == null) return null;
        context.Stock.Remove(stockModel);
        await context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = context.Stock
        .Include(table => table.Comments)
        .AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }
        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await context.Stock.FindAsync(id);
    }

    public async Task<bool> StockExists(int id)
    {
        return await context.Stock.AnyAsync(row => row.Id == id);
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
    {
        Stock? existingStock = await context.Stock.FirstOrDefaultAsync(row => row.Id == id);
        if (existingStock == null)
        {
            return null;
        }
        existingStock.Symbol = stockDTO.Symbol;
        existingStock.CompanyName = stockDTO.CompanyName;
        existingStock.Purchase = stockDTO.Purchase;
        existingStock.LastDiv = stockDTO.LastDiv;
        existingStock.Industry = stockDTO.Industry;
        existingStock.MarketCap = stockDTO.MarketCap;

        await context.SaveChangesAsync();

        return existingStock;
    }
}
