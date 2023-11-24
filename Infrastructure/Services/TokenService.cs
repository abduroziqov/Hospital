using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GenerateRefreshTokensAsync()
        {
            return ComputeSHA256Hash(DateTime.Now.ToString() + "myKey");
        }

        public string ComputeSHA256Hash(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();

        }

        public async Task<Token> GenerateTokensAsync(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim("Id", user.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            double accesTokenLifeTime = double.Parse(_configuration["JWTAccessTokenLifetimeInMinutes"]);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddMinutes(accesTokenLifeTime),
                signingCredentials: credentials,
                claims: claims);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new()
            {
                AccessToken = accessToken,
                RefreshToken = await GenerateRefreshTokensAsync()
            };
        }

        public Task<Token> GetNewTokenFromExpiredTokensAsync(Token tokens)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetClaimsFromExpiredTokenAsync(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var jsonToken = handler.ReadToken(accessToken);

            var claims = (jsonToken as JwtSecurityToken)?.Claims;

            var userClaims = claims?.ToArray();

            return new()
            {
                Id = int.Parse(userClaims.First(x => x.Type.Equals("Id")).Value),
                Name = userClaims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value
            };
        }
    }
}
