using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Buddy2Study.Application.Common;
using Buddy2Study.Application.Dtos;
using Buddy2Study.Application.Interfaces;
using Buddy2Study.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Buddy2Study.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<TokenDto> LoginAsync(string Email, string password)
        {
            var users = await _userRepository.GetUsersDetails(null);

            // Match login username
            var user = users.FirstOrDefault(x => x.Email == Email);
            if (user == null)
            {
                throw new Exception("The username does not match any account.");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Please reset your password and then continue to login.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                throw new Exception("The username or password does not match.");
            }

            // Read JWT settings safely
            var secretKey = _configuration["JWTSettings:SecretKey"]
                ?? throw new Exception("JWT SecretKey is not configured.");
            var issuer = _configuration["JWTSettings:Issuer"]
                ?? throw new Exception("JWT Issuer is not configured.");
            var audience = _configuration["JWTSettings:Audience"]
                ?? throw new Exception("JWT Audience is not configured.");

            var jwtService = new JwtService(secretKey, issuer, audience);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty),
               // new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
                new Claim("UserId", user.Id.ToString())
            };

            var expires = DateTime.UtcNow.AddDays(1);

            var token = jwtService.GenerateToken(claims, expires);

            return new TokenDto
            {
                Token = token,
                ExpiresAt = expires,
              //  RoleName = user.Role,
                Username = user.Email,
                UserId = user.Id
            };
        }
    }



}
