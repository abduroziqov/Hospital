using Application.Services;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Response<(User, Token)>> Register(User user)
            => await _identityService.RegisterAsync(user);

        [HttpPost]
        [AllowAnonymous]
        public async Task<Response<Token>> Login(Credential credentials)
            => await _identityService.LoginAsync(credentials);

        [HttpGet]
        public async Task<Response<bool>> Logout()
            => await _identityService.LogoutAsync();

        [HttpDelete]
        public async Task<Response<bool>> Delete(int userId)
            => await _identityService.DeleteUserAsync(userId);
    }
}
