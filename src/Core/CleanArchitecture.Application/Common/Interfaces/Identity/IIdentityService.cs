using CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUserRoles;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Identity
{
    public interface IIdentityService
    {
        public IQueryable<User> Table { get; }
        Task<User> GetUserByIdAsync(Guid userId);
        Task<string> GetUserNameAsync(Guid userId);
        Task<bool> IsInRoleAsync(Guid userId, string role);
        bool IsInRole(User user, string role);

        // DEPRECATED 
        // Current: Claims are used instead of Policy
        //Task<bool> AuthorizeAsync(Guid userId, string policyName);

        Task<bool> IsInClaimAsync(Guid userId, string claim);


        Task<(ApplicationResult applicationResult, Guid userId)> CreateUserAsync(string userName, string password);
        Task<Guid> CreateUserAsync(User user);
        Task<ApplicationResult> DeleteUserAsync(Guid userId);
        Task<ApplicationResult> UpdateUserAsync(User user);
        Task<bool> AllByConditionAsync(Expression<Func<User, bool>> predicateSelect, Expression<Func<User, bool>> predicateCondition, CancellationToken token = default);
        Task<bool> AllAsync(Expression<Func<User, bool>> predicate, CancellationToken token = default);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<List<string>> GetClaimsByRolesAsync(List<string> userRoles);
        Task<List<User>> GetAllUserAsync();
        Task<ApplicationResult> DeleteRoleAsync(Guid ıd);
        Task<UserRole> GetRoleByIdAsync(Guid id);
        Task<List<Claim>> GetPermissionsAsync(UserRole role);
        Task<Guid> CreateRoleAsync(UserRole userRole);
        Task<ApplicationResult> UpdateRoleAsync(UserRole userRole);
        Task<bool> AllAsync(Expression<Func<UserRole, bool>> predicate, CancellationToken token = default);
        Task<bool> AllByConditionAsync(Expression<Func<UserRole, bool>> predicateSelect, Expression<Func<UserRole, bool>> predicateCondition, CancellationToken token = default);
        Task<List<UserRole>> GetAllRoleAsync();

        Task<ApplicationResult> AddClaimToRole(UserRole role, Claim claim);
        Task<ApplicationResult> RemoveClaimToRole(UserRole role, Claim claim);

        Task UpdateUserRoles(Guid userId, List<UserRoles> userRoles);
        Task<List<User>> GetUsersInRolesAsync(string roleName);
    }
}
