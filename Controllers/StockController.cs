using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using ApiTest.Dtos.StockDTO;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    //Get the general context of the database
    private readonly ApplicationDBContext _context;
    public StockController(ApplicationDBContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Stocks = await _context.Stock.ToListAsync();
        var StocksDTO = Stocks.Select(s => s.ToStockDTO());
        return Ok(StocksDTO);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id) // Get the data passed by the route  (url), and converts it into a integer
    {
        var Stock = await _context.Stock.FindAsync(id);
        if (Stock == null)
        {
            return NotFound();
        }
        return Ok(Stock.ToStockDTO());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDTO) // Get the data from the body and turns it into the DTO
    {
        Stock stockModel = stockDTO.FromCreateToStock(); // Convert the data from the Request DTO to the model, creating a new model instance(in this case a Record)
        await _context.Stock.AddAsync(stockModel); // Add the model to the table of Stocks
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO()); // Return the created model as a response
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
    {
        var stockModel = await _context.Stock.FirstOrDefaultAsync(row => row.Id == id);
        if (stockModel == null)
        {
            return NotFound();
        }
        stockModel.Symbol = updateDTO.Symbol;
        stockModel.CompanyName = updateDTO.CompanyName;
        stockModel.Purchase = updateDTO.Purchase;
        stockModel.LastDiv = updateDTO.LastDiv;
        stockModel.Industry = updateDTO.Industry;
        stockModel.MarketCap = updateDTO.MarketCap;

        await _context.SaveChangesAsync();

        return Ok(stockModel.ToStockDTO());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Stock? stock = await _context.Stock.FirstOrDefaultAsync(row => row.Id == id); // Find the first product that has the same id(Row represents each product, and this acts like a mapper)
        if (stock == null)
        {
            return NotFound();
        }
        _context.Stock.Remove(stock);
        await _context.SaveChangesAsync();
        return NoContent(); // Return no response, but a 201 status
    }
}
