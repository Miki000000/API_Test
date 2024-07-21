using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Dtos.CommentDTO;
using ApiTest.Dtos.CommentDTO;
using ApiTest.Models;

namespace ApiTest.Mappers;

public static class CommentMapper
{
    public static GetCommentRequestDTO toCommentDTO(this Comment comment)
    {
        return new GetCommentRequestDTO
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId
        };
    }
    public static Comment fromCreateToComment(this CreateCommentRequestDTO comment, int stockId)
    {
        return new Comment
        {
            Content = comment.Content,
            Title = comment.Title,
            StockId = stockId
        };
    }
}
