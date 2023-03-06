using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Application.Abstractions.Services
{
    public interface IRoleService
    {
        List<string> GetAllRoles();
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(string id);
        Task<bool> UpdateRoleAsync(string id, string name);
        Task<bool> AddToRoleAsync(string userId, string roleName);
        Task<bool> RemoveFromRoleAsync(string userId, string roleName);
    }
}
