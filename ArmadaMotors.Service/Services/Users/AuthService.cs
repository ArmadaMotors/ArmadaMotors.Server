using ArmadaMotors.Data.IRepositories;
using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs;
using ArmadaMotors.Service.DTOs.Users;
using ArmadaMotors.Service.Exceptions;
using ArmadaMotors.Service.Interfaces.Users;
using FleetFlow.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArmadaMotors.Service.Services.Users
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            _configuration = configuration;
        }

        public async ValueTask<LoginResultDto> AuthenticateAsync(LoginDto dto)
        {
            var user = await this._userRepository.SelectAll()
                .FirstOrDefaultAsync(user => user.Username == dto.Username);

            if (user == null || !PasswordHelper.Verify(dto.Password, user.Password))
                throw new ArmadaException(400, "Username or password is incorrect");

            return new LoginResultDto
            {
                Token = GenerateToken(user)
            };
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
