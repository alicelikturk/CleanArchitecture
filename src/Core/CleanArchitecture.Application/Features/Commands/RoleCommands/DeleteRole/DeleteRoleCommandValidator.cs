using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        private readonly IIdentityService  identityService;

        public DeleteRoleCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Id)
                .MustAsync(DefaultRoleCannotDelete).WithMessage("Varsayılan mağaza silinemez.");
            this.identityService = identityService;
        }

        private async Task<bool> DefaultRoleCannotDelete(DeleteRoleCommand model, Guid roleId, CancellationToken cancellationToken)
        {
            var store = await identityService.GetRoleByIdAsync(roleId);
            return !store.IsDefault;
        }
    }
}
