using Buddy2Study.Application.Common;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Domain.Entities;
using Buddy2Study.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Buddy2Study.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            ISponsorRepository sponsorRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _sponsorRepository = sponsorRepository;
            _configuration = configuration;
        }

        // Normal user login
        public async Task<TokenDto> LoginAsync(string UserName, string password)
        {
            var users = await _userRepository.GetUsersDetails(null);
            var user = users.FirstOrDefault(x => x.UserName == UserName);
            if (user == null) throw new Exception("The username does not match any account.");
            if (string.IsNullOrEmpty(user.PasswordHash)) throw new Exception("Please reset your password.");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("The username or password does not match.");

            // Convert RoleId safely from string to int
            int roleId = int.TryParse(user.RoleId, out var r) ? r : 0;
            string roleName = user.RoleName ?? "Unknown";
            return GenerateToken(user.Id, user.UserName, user.Name, roleId,roleName,user.UserId);
        }
        public async Task<TokenDto> LoginOrRegisterExternalUserAsync(string? email, string? name, string provider)
        {
            // 1. Check if the user exists in DB by email
            var user = await _userRepository.GetByEmailAsync(email);

            // 2. If not, create a new user record
            if (user == null)
            {
                user = new User
                {
                    Username = email ?? name ?? Guid.NewGuid().ToString(),
                    EmailAddress = email,
                    Name = name,
                    AuthProvider = provider,
                    RoleId = 1, // default role (student)
                };
                await _userRepository.GetUsersDetails(user);
            }

            // 3. Generate JWT token (reuse your existing logic)
            return await GenerateToken(user);
        }

        // JWT token generation
        private TokenDto GenerateToken(int id, string userName, string name, int roleId,string roleName,int userId)
        {
            var secretKey = _configuration["JWTSettings:SecretKey"]
                ?? throw new Exception("JWT SecretKey is not configured.");
            var issuer = _configuration["JWTSettings:Issuer"]
                ?? throw new Exception("JWT Issuer is not configured.");
            var audience = _configuration["JWTSettings:Audience"]
                ?? throw new Exception("JWT Audience is not configured.");

            var jwtService = new JwtService(secretKey, issuer, audience);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name ?? string.Empty),
                new Claim("Id", id.ToString()),
                new Claim ("UserId",userId.ToString()),
    
    new Claim("RoleId", roleId.ToString()),
    new Claim("RoleName", roleName ?? string.Empty)

            };

            var expires = DateTime.UtcNow.AddDays(1);
            var token = jwtService.GenerateToken(claims, expires);

            return new TokenDto
            {
                Token = token,
                ExpiresAt = expires,
                RoleId = roleId,
                Username = userName,
                Id = id,
                RoleName=roleName,
                Name=name,
                UserId =userId

            };
        }
    }
}
