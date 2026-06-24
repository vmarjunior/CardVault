using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CardVault.Application.DTOs.Auth;
using CardVault.Application.Interfaces;
using CardVault.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardVault.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(AuthRequestDTO authRequest)
        {
            var user = await _userRepository.GetByAccountNameAsync(authRequest.AccountName);

            if (user == null)
                return null;

            if (!_passwordHasher.VerifyPassword(authRequest.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Domain.Entities.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.AccountName),
                new Claim(JwtRegisteredClaimNames.Name, user.Nickname)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}