using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.DeleteRole
{
    [Authorize(Roles = "Administrator", Claims = "Role.Delete")]
    public class DeleteRoleCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetRoleByIdAsync(request.Id);

            if (user == null)
            {
                return new ServiceResult<bool>(false, new string[] { "Yetki bulunamadı." }, false);
            }

            await identityService.DeleteRoleAsync(user.Id);

            return new ServiceResult<bool>(true, new string[] { }, true);

        }
    }
}
