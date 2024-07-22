using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Interfaces;
using API_A.Repository;
using ApiTest.Data;
using ApiTest.Dtos.StockDTO;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController(IStockRepository stockRepo)
: ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Stock> Stocks = await stockRepo.GetAllAsync();
        var StocksDTO = Stocks.Select(s => s.ToStockDTO());
        return Ok(StocksDTO);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id) // Get the data passed by the route  (url), and converts it into a integer
    {
        Stock? Stock = await stockRepo.GetByIdAsync(id);
        if (Stock == null) return NotFound();
        return Ok(Stock.ToStockDTO());
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDTO) // Get the data from the body and turns it into the DTO
    {
        Stock stockModel = await stockRepo.CreateAsync(stockDTO);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO()); // Return the created model as a response
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
    {
        Stock? stock = await stockRepo.UpdateAsync(id, updateDTO);
        if (stock == null) return NotFound();

        return Ok(stock.ToStockDTO());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Stock? stock = await stockRepo.DeleteAsync(id);
        if (stock == null) return NotFound();
        return NoContent();
    }
}
