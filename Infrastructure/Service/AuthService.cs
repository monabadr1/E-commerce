using Domain.Entities;
using Infrastructure.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var exists = await _context.users.AnyAsync(u => u.Email == dto.Email);
            if (exists)
                throw new Exception("Email already exists");

            var newUser = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                
            };

            _context.users.Add(newUser);
            await _context.SaveChangesAsync();

            return GenerateAuthResponse(newUser);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var dbuser = await _context.users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (dbuser == null)
                throw new Exception("Invalid Email or password");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, dbuser.PasswordHash);
            if (!isPasswordValid)
                throw new Exception("Invalid Email or password");

            return GenerateAuthResponse(dbuser);
        }

        private AuthResponseDto GenerateAuthResponse(User dbuser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, dbuser.UserId.ToString()),
                new Claim(ClaimTypes.Email, dbuser.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, dbuser.Name ?? string.Empty),
                new Claim(ClaimTypes.Role, dbuser.Role ?? "User")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                Token = jwt,
                Email = dbuser.Email ?? string.Empty,
                Name = dbuser.Name ?? string.Empty,
                Role = dbuser.Role ?? "User",
                UserId= dbuser.UserId,
                
            };
        }
    }
}