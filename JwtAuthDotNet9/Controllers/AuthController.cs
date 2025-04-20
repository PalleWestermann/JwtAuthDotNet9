using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null) {
                return BadRequest("Username already exists.");
            }

            return Ok(user);
        }

        // string return result will be a JWT - Json Web Token.
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null) 
            {
                return BadRequest("Invalid username or password");
            }

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }
        
    }
}
