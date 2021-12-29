using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUserRoles;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper mapper;

        public IdentityService(UserManager<User> userManager,
            RoleManager<UserRole> roleManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IMapper mapper)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            this.mapper = mapper;
        }

        // DEPRECATED 
        // Current: Claims are used instead of Policy

        //public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
        //{
        //    var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);

        //    var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        //    var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        //    return result.Succeeded;
        //}

        public IQueryable<User> Table { get => _userManager.Users; }

        public async Task<bool> IsInClaimAsync(Guid userId, string claim)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var claims = principal.Claims.Where(x => x.Type == claim &&
                                                       x.Value == "true" &&
                                                       x.Issuer == "LOCAL AUTHORITY");

            if (claims.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates user who has "User" role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(ApplicationResult applicationResult, Guid userId)> CreateUserAsync(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = userName
            };

            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, "User");

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Guid> CreateUserAsync(User user)
        {
            user.Email = $"{user.UserName}@test.com";
            user.IsEnabled = true;
            user.EmailConfirmed = true;


            var result = await _userManager.CreateAsync(user, "Password@1");

            var defaultRole= roleManager.Roles.FirstOrDefault(x=>x.IsDefault);

            HashSet<string> roles = new HashSet<string> { defaultRole.Name, "User" };


            await _userManager.AddToRolesAsync(user, roles);

            return user.Id;
        }

        public async Task<ApplicationResult> DeleteUserAsync(Guid userId)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return ApplicationResult.Success();
        }

        private async Task<ApplicationResult> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<ApplicationResult> UpdateUserAsync(User user)
        {
            var _user = _userManager.Users.SingleOrDefault(x => x.Id == user.Id);

            if (_user != null)
            {
                _user.UserName = user.UserName;
                _user.Email = user.Email;
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.IsEnabled = user.IsEnabled;

                return await UpdateAsync(_user);
            }

            return ApplicationResult.Success();
        }

        private async Task<ApplicationResult> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return user;
        }

        public async Task<string> GetUserNameAsync(Guid userId)
        {
            var user = await _userManager.Users.FirstAsync(x => x.Id == userId);

            return user.UserName;
        }

        public async Task<bool> IsInRoleAsync(Guid userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(x => x.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public bool IsInRole(User user, string role)
        {
            return _userManager.IsInRoleAsync(user, role).Result;
        }

        public async Task<bool> AllAsync(Expression<Func<User, bool>> predicate, CancellationToken token = default)
        {
            return await _userManager.Users.AllAsync(predicate, token);
        }

        public async Task<bool> AllByConditionAsync(Expression<Func<User, bool>> predicateSelect, Expression<Func<User, bool>> predicateCondition, CancellationToken token = default)
        {
            return await _userManager.Users
                .Where(predicateSelect)
                .AllAsync(predicateCondition, token);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _userManager.Users.ToListAsync();
        }


        #region User Roles
        public async Task<Guid> CreateRoleAsync(UserRole userRole)
        {
            var result = await roleManager.CreateAsync(userRole);

            return userRole.Id;
        }
        public async Task<bool> AllAsync(Expression<Func<UserRole, bool>> predicate, CancellationToken token = default)
        {
            return await roleManager.Roles.AllAsync(predicate, token);
        }

        public async Task<ApplicationResult> UpdateRoleAsync(UserRole role)
        {
            var _role =await GetRoleByIdAsync(role.Id);

            if (_role != null)
            {
                _role.IsDefault = role.IsDefault;
                _role.Name = role.Name;
                _role.Description = role.Description;

                return await UpdateAsync(_role);
            }

            return ApplicationResult.Success();
        }
        private async Task<ApplicationResult> UpdateAsync(UserRole userRole)
        {
            if (userRole.IsDefault)
            {
                var defaultRole=await roleManager.Roles.FirstOrDefaultAsync(x => x.IsDefault);
                if (defaultRole!=null)
                {
                    defaultRole.IsDefault = false;
                    await roleManager.UpdateAsync(defaultRole);
                }
            }

            var result = await roleManager.UpdateAsync(userRole);
            return result.ToApplicationResult();
        }

        public async Task<UserRole> GetRoleByIdAsync(Guid roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());

            return role;
        }

        public async Task<List<Claim>> GetPermissionsAsync(UserRole role)
        {
            var claims = await roleManager.GetClaimsAsync(role);
            return claims.ToList();
        }

        public async Task<ApplicationResult> AddClaimToRole(UserRole role,Claim claim)
        {
            var result = await roleManager.AddClaimAsync(role,claim);
            return result.ToApplicationResult();
        }

        public async Task<ApplicationResult> RemoveClaimToRole(UserRole role, Claim claim)
        {
            var result = await roleManager.RemoveClaimAsync(role, claim);
            return result.ToApplicationResult();
        }

        public async Task<bool> AllByConditionAsync(Expression<Func<UserRole, bool>> predicateSelect, Expression<Func<UserRole, bool>> predicateCondition, CancellationToken token = default)
        {
            return await roleManager.Roles
                .Where(predicateSelect)
                .AllAsync(predicateCondition, token);
        }

        public async Task<ApplicationResult> DeleteRoleAsync(Guid roleId)
        {
            var role = roleManager.Roles.SingleOrDefault(x => x.Id == roleId);

            if (role != null)
            {
                return await DeleteRoleAsync(role);
            }

            return ApplicationResult.Success();
        }

        private async Task<ApplicationResult> DeleteRoleAsync(UserRole  userRole)
        {
            var result = await roleManager.DeleteAsync(userRole);

            return result.ToApplicationResult();
        }

        public async Task<List<UserRole>> GetAllRoleAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<List<User>> GetUsersInRolesAsync(string roleName)
        {
            return (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
        }

        public async Task<List<string>> GetClaimsByRolesAsync(List<string> userRoles)
        {
            var roles = roleManager.Roles.Where(x => userRoles.Contains(x.Name));
            var claims = new List<string>();
            foreach (var role in roles.ToList())
            {
                var _claims = await GetPermissionsAsync(role);
                claims=claims.Concat(_claims.Select(x=>x.Type)).ToList();
            }
            return claims.Distinct().ToList();
        }

        public async Task UpdateUserRoles(Guid userId, List<UserRoles> userRoles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            for (int i = 0; i < userRoles.Count(); i++)
            {
                var role = await roleManager.FindByNameAsync(userRoles[i].RoleName);
                IdentityResult result = null;
                if (userRoles[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!userRoles[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (userRoles.Count - 1))
                        continue;
                    else
                        break;
                }
            }
        }
        #endregion
    }
}
