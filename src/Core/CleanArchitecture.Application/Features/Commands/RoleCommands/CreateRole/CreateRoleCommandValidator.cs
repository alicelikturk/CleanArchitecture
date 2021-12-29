using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.CreateRole
{
    public class CreateRoleCommandValidator:AbstractValidator<CreateRoleCommand>
    {
        private readonly IIdentityService identityService;

        public CreateRoleCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Yetki ismi belirtilmeli.")
                .MaximumLength(200).WithMessage("Yetki ismi 200 karakterden fazla olamaz.")
                .MustAsync(BeUniqueName).WithMessage("Belirtilen yetki ismi zaten mevcut");
            this.identityService = identityService;
        }
        private async Task<bool> BeUniqueName(string roleName, CancellationToken cancellationToken)
        {
            return await identityService
                .AllAsync(x => x.Name != roleName);
        }

    }
}
