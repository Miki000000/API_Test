using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Models;
using ApiTest.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager)
 : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO register)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            AppUser appUser = new AppUser
            {
                UserName = register.UserName,
                Email = register.Email,
            };

            IdentityResult createdUser = await userManager.CreateAsync(appUser, register.Password);

            if (!createdUser.Succeeded) return StatusCode(500, createdUser.Errors);

            var roleResult = await userManager.AddToRoleAsync(appUser, "User");

            if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            return Ok("UserCreated");
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }
}
