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
                UserName = registrationDto.UserName, // Corrected property name
                Email = registrationDto.Email,
                Role = registrationDto.Role,
                ImageUrl = registrationDto.ImageUrl,
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        // User Login
        [HttpPost("login")] // Corrected endpoint name
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
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
                Email = u.Email ?? "" ,
                Role = u.Role,
                ImageUrl = u.ImageUrl,
                CreateDate = u.CreateDate,
            }).ToList();

            return Ok(users);
        }

        // JWT TOKEN GENERATOR
        private string GenerateJwtToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "")); // Corrected class name
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // Corrected syntax

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""), // Ambiguous namespace fixed
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""), // Ambiguous namespace fixed
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
    }
}