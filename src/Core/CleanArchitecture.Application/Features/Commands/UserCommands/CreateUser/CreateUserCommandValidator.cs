using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IIdentityService identityService;

        public CreateUserCommandValidator(IIdentityService identityService)
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Kullanıcı adı belirtilmelidir.")
               .MaximumLength(200).WithMessage("Kullanıcı adı 200 karakterden fazla olamaz.")
               .MustAsync(BeUniqueUserName).WithMessage("Belirtilen kullanıcı adı zaten mevcut");
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim belirtilmelidir.");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim belirtilmelidir.");
           
            this.identityService = identityService;
        }

        private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
        {
            return await identityService
                .AllAsync(x => x.UserName != userName);
        }
      
    }
}
