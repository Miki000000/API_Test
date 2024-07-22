using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_A.Dtos.CommentDTO;

public class UpdateCommentRequestDTO
{
    [Required]
    [MinLength(5, ErrorMessage = "The title must be at least 5 characters")]
    [MaxLength(280, ErrorMessage = "The title cannot exceed 280 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(5, ErrorMessage = "The content must be at least 5 characters")]
    [MaxLength(1600, ErrorMessage = "The content cannot exceed 1600 characters")]
    public string Content { get; set; } = string.Empty;
}
