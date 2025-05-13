using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAppDemo.Configuration;
using WebAppDemo.Models;
using WebAppDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAppDemo.Controllers;

/// <summary>
/// Controlleur utilisé pour récupérer le JWT
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly JwtConfig _jwtConfig;
    private readonly IUserService _userService;

    public AuthenticationController(JwtConfig jwtConfig, IUserService userService)
    {
        _jwtConfig = jwtConfig;
        _userService = userService;
    }

    // POST api/<AuthenticationController>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Valider les informations d'identification
        if (!_userService.ValidateUser(request.Email, request.Password))
        {
            return Unauthorized("Invalid credentials");
        }

        // Générer le token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, "User Name") // Ajouter d'autres claims si nécessaire
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Durée de validité du token
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // Retourner le token au client
        return Ok(new { access_token = tokenString });
    }
}
