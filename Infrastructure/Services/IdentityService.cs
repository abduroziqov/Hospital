using Application.Services;
using Domain.Entities;
using Domain.Models;
using Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly HospitalDbContext _dbContext;
        private readonly int _refreshTokenLifetime;

        public IdentityService(ITokenService tokenService, HospitalDbContext hospitalDbContext, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _dbContext = hospitalDbContext;
            _refreshTokenLifetime = int.Parse(configuration["JWT:RefreshTokenLifetimeInMinutes"]);
        }

        public Task<Response<bool>> DeleteUserAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidRefreshToken(string refreshToken, int userId)
        {
            RefreshToken refreshTokenEntity;

            var res = _dbContext.RefreshTokens.Where(x => x.UserId.Equals(userId) &&
            x.RefreshTokenValue.Equals(refreshToken));
            if (res.Count() != 1)
                return false;

            refreshTokenEntity = res.First();
            if(refreshTokenEntity.ExpireTime < DateTime.Now)
                return false;

            return true;
        }

        public async Task<Response<Token>> LoginAsync(Credential credential)
        {
            // Name     UserName
            credential.Password = _tokenService.ComputeSHA256Hash(credential.Password);
            User? user = _dbContext.Users.FirstOrDefault(x => x.Name.Equals(credential.Name) &&
                                                              x.Password.Equals(credential.Password));

            if(user == null)
                return new("User not found!", 404);

            Token userToken = await _tokenService.GenerateTokensAsync(user);

            bool isSuccess = await SaveRefreshToken(userToken.RefreshToken, user);
            return isSuccess ? new(userToken) : new("Failed to save token!");
        }

        public async Task<Response<bool>> LogoutAsync()
        {
            // remove from database
            return new(true);
        }

        public async Task<Response<Token>> RefreshTokenAsync(Token token) 
        {
            User user = await _tokenService.GetClaimsFromExpiredTokenAsync(token.AccessToken);

            if (!await IsValidRefreshToken(token.RefreshToken, user.Id))
                return new("Refresh token invalid!");

            Token newToken = await _tokenService.GenerateTokensAsync(user);
            bool isSuccess = await SaveRefreshToken(newToken.RefreshToken, user);

            return isSuccess ? new(newToken) : new("Failed");
        }

        public async Task<Response<(User, Token)>> RegisterAsync(User user)
        {
            // validation dates

            user.Password = _tokenService.ComputeSHA256Hash(user.Password);

            await _dbContext.Users.AddAsync(user);
            int effectedRows = _dbContext.SaveChanges();

            if (effectedRows <= 0)
                return new("Operation failed");

            Token token = await _tokenService.GenerateTokensAsync(user);
            bool isSuccess =  await SaveRefreshToken(token.RefreshToken, user);
            return isSuccess ? new((user, token)) : new("Failed");
        }

        public async Task<bool> SaveRefreshToken(string refreshToken, User user)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            RefreshToken refreshTokenEntity;
            var res = _dbContext.RefreshTokens.Where(x => x.UserId.Equals(user.Id) &&
            x.RefreshTokenValue.Equals(refreshToken));

            if(res.Count() == 0)
            {
                refreshTokenEntity = new()
                {
                    ExpireTime = DateTime.Now.AddMinutes(_refreshTokenLifetime),
                    RefreshTokenValue = refreshToken,
                    UserId = user.Id,
                };
               await _dbContext.RefreshTokens.AddAsync(refreshTokenEntity);
            }
            else if (res.Count() == 1)
            {
                refreshTokenEntity = res.First();
                refreshTokenEntity.RefreshTokenValue = refreshToken;
                refreshTokenEntity.ExpireTime = DateTime.Now.AddMinutes(_refreshTokenLifetime);

                _dbContext.RefreshTokens.Update(refreshTokenEntity);
            }
            else
                return false;

            int rows = await _dbContext.SaveChangesAsync();

            return rows > 0;
        }
    }
}
