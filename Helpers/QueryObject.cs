using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Helpers;

public class QueryObject
{
    public string? Symbol { get; set; } = null;
    public string? CompanyName { get; set; } = null;
    [AllowedValues(["CompanyName", "Symbol", null], ErrorMessage = "The values should be or 'CompanyName' or 'Symbol'")]
    public string? SortBy { get; set; } = null;
    public bool IsDescencing { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
