using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API_A.Interfaces;
using API_A.Models;
using Microsoft.IdentityModel.Tokens;

namespace API_A.Services;

/// <summary>
/// A class that handles the web tokens utilized by the user in each season.<br/>
/// The token will have the Email and Name registered on itself, faciliting the client validations without having to constantly check the database.<br/>
/// The class set the configurations such as time of expiration, signing credentials, and others.
/// </summary>
/// <param name="config">Json Application Configuration</param>
public class TokenService(IConfiguration config)
: ITokenService
{
    private readonly SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SigningKey"]!));
    /// <summary>
    /// Creates a session token using the user information provided in the AppUser entity.<br/>
    /// The token will contain non-sensitive information such as the user's email and name.
    /// </summary>
    /// <param name="user">The user entity containing user information</param>
    /// <returns>A string containing the JWT (JSON Web Token)</returns>
    public string CreateToken(AppUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
