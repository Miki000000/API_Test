using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Dtos.CommentDTO;
using ApiTest.Models;

namespace ApiTest.Mappers;

public static class CommentMapper
{
    public static GetCommentRequestDTO toCommentDTO(this Comment comment)
    {
        return new GetCommentRequestDTO
        {
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn
        };
    }
}
