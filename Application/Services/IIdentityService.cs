using Domain.Entities;
using Domain.Models;

namespace Application.Services;

public interface IIdentityService
{
    Task<Response<(User, Token)>> RegisterAsync (User user);
    Task<Response<Token>> LoginAsync(Credential credential);
    Task<Response<bool>> LogoutAsync();
    Task<Response<Token>> RefreshTokenAsync(Token token);
    Task<Response<bool>> DeleteUserAsync(int UserId);
    Task<bool> SaveRefreshToken(string refreshToken, User user);
    Task<bool> IsValidRefreshToken(string refreshToken, int userId);
}
