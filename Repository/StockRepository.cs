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
    /// <summary>
    /// The method <c>CreateAsync</c> create in the Stock Table a item following the CreateStockRequestDTO passed by parameter.
    /// </summary>
    /// <param name="_stockModel">The parameter recieve every property necessary to create a Stock item</param>
    /// <returns>Stock created</returns>
    public async Task<Stock> CreateAsync(CreateStockRequestDTO _stockModel)
    {
        Stock stockModel = _stockModel.FromCreateToStock();
        await context.Stocks.AddAsync(stockModel);
        await context.SaveChangesAsync();
        return stockModel;
    }

    /// <summary>
    ///The method <c>DeleteAsync</c> gets a ID by its parameter, and upon searching and confirming it's existence in the Stock Table<br/>
    ///It delets the item from the Stock Table.
    /// </summary>
    /// <param name="id">Id of the item to be deleted</param>
    /// <returns>If the item has been successfully deleted, returns the item, otherwise, returns null</returns>
    public async Task<Stock?> DeleteAsync(int id)
    {
        Stock? stockModel = await context.Stocks.FirstOrDefaultAsync(row => row.Id == id);
        if (stockModel == null) return null;
        context.Stocks.Remove(stockModel);
        await context.SaveChangesAsync();
        return stockModel;
    }

    /// <summary>
    /// Recieves a QueryObject instance as a param and, based on the query, it threat the data
    /// on the specified way, verifying if it has any of the specific sorting or filtering camps.<br/> Then returning
    /// the data from the Stock Table after this threatment.<br/>
    /// </summary>
    /// <param name="query">An object containing query parameters for filtering, sorting, and pagination.</param>
    /// <returns>A list of stocks after passing by the filters, sorting and pagination.</returns>
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        //Get the database through the context database connection, including the comments through a join SQL query, turning it into
        //queryable so it can accept queries after.
        var stocks = context.Stocks
        .Include(table => table.Comments)
        .Include(table => table.Portfolios)
        .AsQueryable();

        //Checks if the query passed in the class has the filters "CompanyName" or "Symbol", and filter by that if they do.
        stocks = !string.IsNullOrWhiteSpace(query.CompanyName) ? stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName)) : stocks;
        stocks = !string.IsNullOrWhiteSpace(query.Symbol) ? stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol)) : stocks;

        // Checks if a sorting was passed in the query
        stocks = query.SortBy switch
        {
            "Symbol" => query.IsDescencing ? stocks.OrderBy(s => s.Symbol) : stocks.OrderByDescending(s => s.Symbol),
            "CompanyName" => query.IsDescencing ? stocks.OrderByDescending(s => s.CompanyName) : stocks.OrderBy(s => s.CompanyName),
            _ => stocks
        };
        //Calcs the number of pages the client gonna get, and the number of elements in each page
        int skipNumber = (query.PageNumber - 1) * query.PageSize;
        return await stocks
        .Skip(skipNumber).Take(query.PageSize)
        .ToListAsync();
    }

    /// <summary>
    /// Get the Stock from the StockTable based on its id.
    /// </summary>
    /// <param name="id">Stock id.</param>
    /// <returns>Stock</returns>
    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await context.Stocks.FindAsync(id);
    }
    /// <summary>
    /// Checks if the stock exists on the table.
    /// </summary>
    /// <param name="id">Stock id.</param>
    /// <returns>Boolean, true if the stock exists, false if it don't</returns>
    public async Task<bool> StockExists(int id)
    {
        return await context.Stocks.AnyAsync(row => row.Id == id);
    }

    /// <summary>
    /// Update the stock content in the Stocks Table based on the UpdateStockRequestDTO properties.
    /// </summary>
    /// <param name="id">Stock id.</param>
    /// <param name="stockDTO">Stock instance</param>
    /// <returns>The stock after the changes</returns>
    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockDTO)
    {
        Stock? existingStock = await context.Stocks.FirstOrDefaultAsync(row => row.Id == id);
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