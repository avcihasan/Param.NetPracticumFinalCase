using Microsoft.AspNetCore.Identity;
using ProductTracking.Application.Abstractions.Services;
using ProductTracking.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracking.Persistence.Services
{
    public class RoleService:IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        public RoleService(RoleManager<AppRole> roleManager,  UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
           AppUser user=await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");
            AppRole role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new Exception("Rol bulunamadı");

            IdentityResult result=await _userManager.AddToRoleAsync(user,roleName);
            return result.Succeeded;
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });

            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            AppRole appRole = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(appRole);
            return result.Succeeded;
        }

        public List<string> GetAllRoles()
            => _roleManager.Roles.Select(x => x.Name).ToList();

        public async Task<bool> RemoveFromRoleAsync(string userId, string roleName)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı");
            AppRole role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new Exception("Rol bulunamadı");

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<bool> UpdateRoleAsync(string id, string name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
