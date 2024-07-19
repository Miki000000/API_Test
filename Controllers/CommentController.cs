using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Data;
using ApiTest.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Route("api/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    public CommentController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Comments = await _context.Comments.ToListAsync();
        var CommentsDTO = Comments.Select(row => row.toCommentDTO());
        return Ok(CommentsDTO);
    }
}
