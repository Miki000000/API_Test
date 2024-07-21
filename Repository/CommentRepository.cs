using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Dtos.CommentDTO;
using API_A.Interfaces;
using ApiTest.Data;
using ApiTest.Mappers;
using ApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace API_A.Repository;

public class CommentRepository(ApplicationDBContext context) : ICommentRepository
{

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        Comment? comment = await context.Comments.FirstOrDefaultAsync(row => row.Id == id);
        if (comment == null) return null;
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await context.Comments.FirstOrDefaultAsync(row => row.Id == id);
    }
}
