using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace ApiTest.Models;
public class CommentModel
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? StockId { get; set; }
    public StockModel? Stock { get; set; }
}
