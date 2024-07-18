using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using ApiTest.Dtos.StockDTO;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetAll()
    {
        var Stocks = _context.Stock.ToList()
        .Select(s => s.ToStockDTO());

        return Ok(Stocks);
    }
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var Stock = _context.Stock.Find(id);
        if (Stock == null)
        {
            return NotFound();
        }
        return Ok(Stock.ToStockDTO());
    }
    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDTO stockDTO) // Get the data from the body and turns it into the DTO
    {
        Stock stockModel = stockDTO.FromCreateToStock(); // 
        _context.Stock.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO());
    }
}
