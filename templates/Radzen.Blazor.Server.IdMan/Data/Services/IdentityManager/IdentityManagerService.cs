using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radzen.Blazor.Server.IdMan.Data.Services
{
    public class IdentityManagerService
    {
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private readonly UserManager<IdentityUser> UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        //private AuthenticationStateProvider AuthenticationStateProvider;
        
        public IdentityManagerService(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            UserManager = usermanager;
            RoleManager = rolemanager;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {

            List<IdentityUser> Users = await UserManager.Users.ToListAsync();
            return Users;
        }

        public async Task<IdentityUser> GetUserById(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public async Task<IList<IdentityRole>> GetAllRolesAsync()
        {
            return await RoleManager.Roles.ToListAsync();
        }

        public async Task<bool> CreateRoleAsync(string name)
        {
            // If the role name already exists Succeeded will be false
            var x = await RoleManager.CreateAsync(new IdentityRole(name));

            return x.Succeeded;
        }

        // Delete a Role 
        /*
         * Should not do this really but needless to say that it:
         * - Deletes role that is assigned to user -> AspNetUserRoles
         * - Deletes the role's claims -> AspNetRoleClaims
         * - Deletes the role itself -> AspNetRoles
         * - I will keep it as private to prevent mishaps!
         */
        private async Task<bool> RemoveRoleAsync(string name)
        {
            bool success = false;
            var role = await RoleManager.FindByNameAsync(name);
            if (role != null)
            {
                var isDeleted = await RoleManager.DeleteAsync(role);
                success = isDeleted.Succeeded;
            }
            return success;
        }

        public async Task<bool> AddUserToRoleAsync(string userName, string roleName)
        {
            bool success = false;
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var isAdded = await UserManager.AddToRoleAsync(user, roleName);
                success = isAdded.Succeeded;
            }
            return success;
        }

        public async Task<bool> AddUserToRoleAsync(IdentityUser user, string roleName)
        {
            bool success = false;
            if (user != null)
            {
                var isAdded = await UserManager.AddToRoleAsync(user, roleName);
                success = isAdded.Succeeded;
            }
            return success;
        }

        public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string role)
        {
            return await UserManager.GetUsersInRoleAsync(role);
        }

    }
}