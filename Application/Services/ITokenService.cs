using Domain.Entities;
using Domain.Models;

namespace Application.Services;

public interface ITokenService
{
     Task<Token> GenerateTokensAsync(User user);
     Task<User> GetClaimsFromExpiredTokenAsync(string accessToken);
    // Task<Token> GenerateRefreshTokensAsync();
     Task<string> GenerateRefreshTokensAsync();
     Task<Token> GetNewTokenFromExpiredTokensAsync(Token tokens);
    string ComputeSHA256Hash(string input);
}
