using Microsoft.AspNetCore.Mvc;
using Alloca8._2.Data;
using Microsoft.AspNetCore.Identity;
using Alloca8._2.Models.Entities;
using Alloca8._2.Dtos;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization; // Added for Authorization

namespace Alloca8._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly Alloca8DbContext _context;
        private readonly IConfiguration _configuration; // Corrected name

        public UsersController(UserManager<Users> userManager, Alloca8DbContext context, IConfiguration configuration) // Corrected parameter name
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        // User Registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            var user = new Users
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                Role = registrationDto.Role,
                OwnerID = registrationDto.OwnerID 
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully", ownerID = user.OwnerID }); // Return ownerID
            }

            return BadRequest(result.Errors);
        }

        // User Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            try
            {
                Console.WriteLine($"Login attempt: Email={loginDto.Email}"); // Log email
                var user = await _userManager.FindByEmailAsync(loginDto.Email); // Retrieve user

                if (user != null)
                {
                    Console.WriteLine($"User found: UserName={user.UserName}, UserId={user.Id}"); // Log user info
                    var passwordCheckResult = await _userManager.CheckPasswordAsync(user, loginDto.Password); // Verify password
                    Console.WriteLine($"Password check result: {passwordCheckResult}"); // Log password check result

                    if (passwordCheckResult)
                    {
                        var token = GenerateJwtToken(user);
                        Console.WriteLine($"JWT generated successfully.");
                        return Ok(new { Token = token, OwnerID = user.OwnerID, Role = user.Role });
                    }
                    else
                    {
                        Console.WriteLine($"Login failed: Invalid password for {loginDto.Email}");
                        return Unauthorized("Invalid email or password.");
                    }
                }
                else
                {
                    Console.WriteLine($"Login failed: User not found for {loginDto.Email}");
                    return Unauthorized("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message} {ex.StackTrace}");
                return StatusCode(500, $"Internal server error: {ex.Message}"); // Include exception message
            }
        }
        // Get All Users
        [HttpGet]
        [Authorize(Roles = "Admin")] // Example: Only Admins can access this endpoint
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _context.Users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName ?? "",
                Email = u.Email ?? "",
                Role = u.Role,
                CreateDate = u.CreateDate,
            }).ToList();

            return Ok(users);
        }

        // JWT TOKEN GENERATOR

        private string GenerateJwtToken(Users user)
        {
            try
            {
                Console.WriteLine($"Generating JWT for {user.Email}");
                Console.WriteLine($"Jwt:Key: {_configuration["Jwt:Key"]}");
                Console.WriteLine($"Jwt:Issuer: {_configuration["Jwt:Issuer"]}");
                Console.WriteLine($"Jwt:Audience: {_configuration["Jwt:Audience"]}");
                Console.WriteLine($"User.UserName: {user.UserName}");
                Console.WriteLine($"User.Email: {user.Email}");
                Console.WriteLine($"User.Id: {user.Id}");
                Console.WriteLine($"User.Role: {user.Role}");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Role, user.Role.ToString() ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? "")
        };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"] ?? "",
                    _configuration["Jwt:Issuer"] ?? "",
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JWT error: {ex.Message} {ex.StackTrace}");
                throw;
            }
        }
    }
}