using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.PermissionStoreCommands.CreatePermissionStore
{
    public class CreatePermissionStoreCommandValidator:AbstractValidator<CreatePermissionStoreCommand>
    {
        private readonly IPermissionStoreRepository permissionStoreRepository;

        public CreatePermissionStoreCommandValidator(IPermissionStoreRepository permissionStoreRepository)
        {
            RuleFor(x => x.ClaimType)
               .NotEmpty().WithMessage("İzin türü belirtilmeli.")
               .MaximumLength(200).WithMessage("İzin türü 200 karakterden fazla olamaz.")
               .MustAsync(BeUniqueName).WithMessage("Belirtilen İzin türü zaten mevcut");
            this.permissionStoreRepository = permissionStoreRepository;
        }
        private async Task<bool> BeUniqueName(string claimType, CancellationToken cancellationToken)
        {
            return await permissionStoreRepository
                .AllAsync(x => x.ClaimType != claimType);
        }
    }
}
