using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest.Dtos.Account;

public class RegisterDTO
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
