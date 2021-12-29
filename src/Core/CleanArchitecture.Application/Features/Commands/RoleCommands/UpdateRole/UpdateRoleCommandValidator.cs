using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.UpdateRole
{
    public class UpdateRoleCommandValidator:AbstractValidator<UpdateRoleCommand>
    {
        private readonly IIdentityService identityService;

        public UpdateRoleCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yetki ismi belirtilmeli.")
                .MaximumLength(200).WithMessage("Yetki ismi 200 karakterden fazla olamaz.")
                .MustAsync(BeUniqueName).WithMessage("Belirtilen yetki ismi zaten mevcut");
            this.identityService = identityService;
        }
        private async Task<bool> BeUniqueName(UpdateRoleCommand model, string roleName, CancellationToken cancellationToken)
        {
            return await identityService
                .AllByConditionAsync(x => x.Id != model.Id, y => y.Name != roleName);
        }
    }
}
