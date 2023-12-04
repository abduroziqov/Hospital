using Application.Services;
using Domain.Entities;
using Domain.Models;
using Infrastructure.DataAccess;

namespace Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly HospitalDbContext _dbContext;

        public RoleService(HospitalDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /*public async Task<Response<(User, Token)>> RegisterRoleAsync(Role role)
        {
            try
            {
                Role roles = new Role();
                roles.Id = role.Id;
                roles.Name = role.Name;

                _dbContext.Roles.Add(roles);
                await _dbContext.SaveChangesAsync();

                return new("Role registered succesfully!");
            }
            catch (Exception ex)
            {
                return new($"Error registering role: {ex.Message}");
            }
        }*/

        public async Task<Response<bool>> DeleteRoleAsync(int roleId)
        {
            try
            {

            }
            catch(Exception ex)
            {
                return new($"Error deleting role: {ex.Message}");
            }
            return null;
        }

        public async Task<Role> RegisterRoleAsync(Role role)
        {
            Role role1 = new Role();
            role1.Id = role.Id;
            role1.Name = role.Name;

            _dbContext.Roles.AddAsync(role1);
            await _dbContext.SaveChangesAsync();

            return role1;
        }
    }
}