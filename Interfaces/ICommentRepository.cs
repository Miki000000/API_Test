using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Dtos.CommentDTO;
using ApiTest.Models;

namespace API_A.Interfaces;

public interface ICommentRepository
{
    public Task<Comment> CreateAsync(Comment comment);
    public Task<List<Comment>> GetAllAsync();
    public Task<Comment?> GetByIdAsync(int id);
    public Task<Comment?> DeleteAsync(int id);
}
