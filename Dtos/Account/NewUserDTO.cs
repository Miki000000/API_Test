using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_A.Dtos.Account;

public class NewUserDTO
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
