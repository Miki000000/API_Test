using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    /// <summary>
    /// Method <c>GetAllAsync</c> recieves a QueryObject instance as a param and, based on the query, it threat the data
    /// on the specified way, verifying if it has any of the specific sorting or filtering camps.<br/> Then returning
    /// the data after this threatment.<br/>
    /// </summary>
    /// <param name="query">An object containing query parameters for filtering, sorting, and pagination.</param>
    /// <returns>A list of stocks after passing by the filters, sorting and pagination.</returns>
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        //Get the database through the context database connection, including the comments through a join SQL query, turning it into
        //queryable so it can accept queries after.
        var stocks = context.Stock
        .Include(table => table.Comments)
        .AsQueryable();

        //Checks if the query passed in the class has the filters "CompanyName" or "Symbol", and filter by that if they do.
        stocks = !string.IsNullOrWhiteSpace(query.CompanyName) ? stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName)) : stocks;
        stocks = !string.IsNullOrWhiteSpace(query.Symbol) ? stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol)) : stocks;

        //Checks if a sorting was passed in the query
        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            //Checks if the method passed in the SortBy property is "Symbol" or "CompanyName", and then sort it based in if it is descending or not by
            //By another property called "IsDescending" which is a boolean
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescencing ? stocks.OrderBy(s => s.Symbol) : stocks.OrderByDescending(s => s.Symbol);
            }
            if (query.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescencing ? stocks.OrderBy(s => s.CompanyName) : stocks.OrderByDescending(s => s.CompanyName);
            }
        }
        //Calcs the number of pages the client gonna get, and the number of elements in each page
        int skipNumber = (query.PageNumber - 1) * query.PageSize;
        return await stocks
        .Skip(skipNumber).Take(query.PageSize)
        .ToListAsync();
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