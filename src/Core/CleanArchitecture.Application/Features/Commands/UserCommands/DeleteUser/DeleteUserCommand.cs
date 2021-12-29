using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.DeleteUser
{
    [Authorize(Roles = "Administrator", Claims = "User.Delete")]
    public class DeleteUserCommand:IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.Id);

            if (user==null)
            {
                return new ServiceResult<bool>(false, new string[] { "Satış temsilcisi bulunamadı." },false);
            }

            await identityService.DeleteUserAsync(user.Id);

            return new ServiceResult<bool>(true, new string[] { },true);

        }
    }
}
