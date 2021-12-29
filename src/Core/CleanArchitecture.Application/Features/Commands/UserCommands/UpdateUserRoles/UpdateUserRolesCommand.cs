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

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUserRoles
{
    [Authorize(Roles = "Administrator", Claims = "User.UpdateUserRoles")]
    public class UpdateUserRolesCommand:IRequest<ServiceResult<bool>>
    {
        public Guid UserId { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;

        public UpdateUserRolesCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<ServiceResult<bool>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            await identityService.UpdateUserRoles(request.UserId,request.UserRoles);

            return new ServiceResult<bool>(true, new string[] { }, true);
        }
    }
}
