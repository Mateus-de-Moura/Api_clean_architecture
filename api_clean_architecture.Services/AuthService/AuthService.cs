using api_clean_architecture.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api_clean_architecture.Services.AuthService
{
    public class AuthService(IConfiguration configuration) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        public string GenerateJWT(string email, string username)
        {
            var issuer = _configuration["JWT:Issuer"];
            var audience = _configuration["JWT:Audience"];
            var key = _configuration["JWT:key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new("Email", email),
                new("Username",username),
                new("EmailIdentifier", email.Split("@").ToString()!),
                new("CurrenTime", DateTime.Now.ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
                claims: claims, expires: DateTime.Now.AddDays(7), signingCredentials: credentials);

            var tokenHendler = new JwtSecurityTokenHandler();

            return tokenHendler.WriteToken(token);  
        }

        public string GenerateRefreshToken()
        {
            var securityRandomBytes = new byte[128];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(securityRandomBytes);

            return Convert.ToBase64String(securityRandomBytes);
        }
    }
}
