using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IIdentityService identityService;

        public UpdateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Kullanıcı adı belirtilmeli.")
               .MaximumLength(200).WithMessage("Kullanıcı adı 200 karakterden fazla olamaz.")
               .MustAsync(BeUniqueUserName).WithMessage("Belirtilen kullanıcı adı zaten mevcut");
            this.identityService = identityService;
        }

        private async Task<bool> BeUniqueUserName(UpdateUserCommand model, string userName, CancellationToken cancellationToken)
        {
            return await identityService
                .AllByConditionAsync(x => x.Id != model.Id, y => y.UserName != userName);
        }
    }
}
