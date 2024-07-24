using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_A.Dtos.Account;
using API_A.Interfaces;
using API_A.Models;
using ApiTest.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signManager)
 : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        AppUser? user = await userManager.Users.FirstOrDefaultAsync(user => user.UserName!.ToLower() == login.Username.ToLower());
        if (user == null) return Unauthorized("Invalid Username");

        var result = await signManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid Password");

        return Ok(new NewUserDTO
        {
            UserName = user.UserName!,
            Email = user.Email!,
            Token = tokenService.CreateToken(user)
        }
        );
    }
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

            IdentityResult roleResult = await userManager.AddToRoleAsync(appUser, "User");

            if (!roleResult.Succeeded) return StatusCode(500, roleResult.Errors);

            return Ok(new NewUserDTO
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Token = tokenService.CreateToken(appUser)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "An error occurred while processing your request", Details = e.Message });
        }
    }
}
