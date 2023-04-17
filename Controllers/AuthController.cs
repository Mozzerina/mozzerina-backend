using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mozzerina.Data;
using Mozzerina.Data.DTO;
using Mozzerina.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mozzerina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MozzerinaContext _dataContext;
        private readonly IConfiguration _configuration;
        public AuthController(MozzerinaContext dataContext, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _configuration = configuration;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Login(RegisterDto request)
        {
            User? temp = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (temp != null)
            {
                return BadRequest("This email is already exist");
            }
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            User user = new()
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = passwordHash
            };
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            User? user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("Email aren`t exist");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password");
            }
            string token = CreateToken(user);

            return Ok(token);
        }
    }
}
