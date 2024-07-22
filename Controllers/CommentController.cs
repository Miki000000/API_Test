using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Dtos.CommentDTO;
using API_A.Interfaces;
using ApiTest.Data;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
: ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await commentRepo.GetAllAsync();
        var commentsDTO = comments.Select(row => row.toCommentDTO());
        return Ok(commentsDTO);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Comment? comment = await commentRepo.GetByIdAsync(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment.toCommentDTO());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Comment? comment = await commentRepo.DeleteAsync(id);
        if (comment == null) return NotFound();
        return NoContent();
    }
    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDTO _comment)
    {
        if (!await stockRepo.StockExists(stockId)) return BadRequest("Stock does not exist!");
        Comment commentModel = _comment.fromCreateToComment(stockId);
        await commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.toCommentDTO());
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDTO _comment)
    {
        Comment? comment = await commentRepo.UpdateAsync(id, _comment);
        if (comment == null) return NotFound();
        return Ok(comment.toCommentDTO());
    }
}
