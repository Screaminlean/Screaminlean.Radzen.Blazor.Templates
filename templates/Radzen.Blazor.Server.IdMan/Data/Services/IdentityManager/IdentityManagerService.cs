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

        public async Task<bool> CreateNewUserAsync(string username, string email, string passwordhash)
        {
            IdentityUser ExistingUser = await UserManager.FindByEmailAsync(email);

            if (null == ExistingUser)
            {
                var User = new IdentityUser {
                    UserName = username, Email = email};
                var CreatedResult = await UserManager.CreateAsync(User, passwordhash);
                return CreatedResult.Succeeded;
            }

            return false;

        }

        public async Task<bool> UpdateUserLockAsync(string id, bool islocked)
        {
            IdentityUser ExistingUser = await GetUserByIdAsync(id);

            if (null == ExistingUser)
            {
                return false;
            }
            var UpdateLock = await UserManager.SetLockoutEnabledAsync(ExistingUser, islocked);
            var UpdateExpire = await UserManager.SetLockoutEndDateAsync(ExistingUser, DateTime.Now);

            return UpdateLock.Succeeded && UpdateExpire.Succeeded;
        }

        public async Task<bool> UpdateUserPassword(string id, string password)
        {
            IdentityUser ExistingUser = await GetUserByIdAsync(id);
            if (null == ExistingUser)
            {
                return false;
            }

            var token = await UserManager.GeneratePasswordResetTokenAsync(ExistingUser);
            var Updated = await UserManager.ResetPasswordAsync(ExistingUser, token, password);

            return Updated.Succeeded;
        }

        // String overload for flexablity
        public async Task<bool> DeleteUserAsync(string userid)
        {
            IdentityUser User = await GetUserByIdAsync(userid);
            return await DeleteUserAsync(User);
        }

        public async Task<bool> DeleteUserAsync(IdentityUser user)
        {
            IdentityUser ExistingUser = await GetUserByIdAsync(user.Id);
            if (ExistingUser != null)
            {
                var Result = await UserManager.DeleteAsync(user);
                return Result.Succeeded;
            }
            return false;
        }

        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            List<IdentityUser> Users = await UserManager.Users.ToListAsync();
            return Users;
        }

        public async Task<IdentityUser> GetUserByIdAsync(string id)
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
            var CreateRole = await RoleManager.CreateAsync(new IdentityRole(name));

            return CreateRole.Succeeded;
        }

        // Delete a Role 
        /*
         * Should not do this really but needless to say that it:
         * - Deletes role that is assigned to user -> AspNetUserRoles
         * - Deletes the role's claims -> AspNetRoleClaims
         * - Deletes the role itself -> AspNetRoles
         * - I will keep it as private to prevent mishaps!
         */
        public async Task<bool> DeleteRoleAsync(string id)
        {
            var Role = await RoleManager.FindByIdAsync(id);
            if (Role != null)
            {
                var DeleteRole = await RoleManager.DeleteAsync(Role);
                return DeleteRole.Succeeded;
            }
            return false;
        }

        public async Task<bool> AddUserToRoleAsync(string userName, string roleName)
        {
            IdentityUser User = await UserManager.FindByNameAsync(userName);

            if (User != null)
            {
                var AddToRole = await UserManager.AddToRoleAsync(User, roleName);
                return AddToRole.Succeeded;
            }
            return false;
        }

        public async Task<bool> RemoveUserFromRole(string id, string role)
        {
            IdentityUser User = await GetUserByIdAsync(id);
            var RemoveFromRole = await UserManager.RemoveFromRoleAsync(User, role);
            return RemoveFromRole.Succeeded;
        }

        public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string role)
        {
            return await UserManager.GetUsersInRoleAsync(role);
        }

        public async Task<bool> IsUserInRole(string id, string role)
        {
            IdentityUser User = await GetUserByIdAsync(id);
            return await UserManager.IsInRoleAsync(User, role);
        }

    }
}