using Application.Services;
using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly HospitalDbContext _hospitalDbContext;


        public RoleController(IRoleService roleService, HospitalDbContext hospitalDbContext)
        {
            _roleService = roleService;
            _hospitalDbContext = hospitalDbContext;
        }

        [HttpPost]
        /*public async Task<Role> Register(Role role)
        {
            return null;
        }*/

        [HttpPost]
        public async Task<IActionResult> RegisterRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Role cannot be null");
            }

            try
            {
                var registeredRole = await _roleService.RegisterRoleAsync(role);
                return Ok(registeredRole);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error registering role: {ex.Message}");
            }
        }
    }
}
