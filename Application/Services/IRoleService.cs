using Domain.Entities;
using Domain.Models;

namespace Application.Services;

public interface IRoleService
{
    Task<Role> RegisterRoleAsync(Role role);
    Task<Response<bool>> DeleteRoleAsync(int roleId);
}
