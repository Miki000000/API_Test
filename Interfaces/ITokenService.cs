using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Models;

namespace API_A.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
