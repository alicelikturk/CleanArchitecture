using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.RemoveClaimToRole
{
    public class RemoveClaimToRoleCommandValidator : AbstractValidator<RemoveClaimToRoleCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IPermissionStoreRepository permissionStoreRepository;

        public RemoveClaimToRoleCommandValidator(IIdentityService identityService, IPermissionStoreRepository permissionStoreRepository)
        {
            this.identityService = identityService;
            this.permissionStoreRepository = permissionStoreRepository;

            RuleFor(x => x.RoleId)
               .MustAsync(BeNotNullRole).WithMessage("Yetki bulunamadı.");
            RuleFor(x => x.PermissionStoreId)
              .MustAsync(BeNotNullPermission).WithMessage("İzin bulunamadı.");
        }

        private async Task<bool> BeNotNullPermission(Guid permissionId, CancellationToken cancellationToken)
        {
            var permissionStore = await permissionStoreRepository.GetByIdAsync(permissionId);
            return permissionStore != null ? true : false;
        }

        private async Task<bool> BeNotNullRole(Guid roleId, CancellationToken cancellationToken)
        {
            var role = await identityService.GetRoleByIdAsync(roleId);
            return role != null ? true : false;
        }
       
    }
}
