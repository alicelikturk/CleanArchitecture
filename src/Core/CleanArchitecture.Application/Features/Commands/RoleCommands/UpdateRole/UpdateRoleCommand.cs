using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.UpdateRole
{
    [Authorize(Roles = "Administrator", Claims = "Role.Update")]
    public class UpdateRoleCommand:IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public CreateRoleCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await identityService.GetRoleByIdAsync(request.Id);

            if (role == null)
            {
                return new ServiceResult<bool>(false, new string[] { "Yetki bulunamadı." }, false);
            }

            await identityService.UpdateRoleAsync(mapper.Map<UserRole>(request));

            return new ServiceResult<bool>(true, new string[] { }, true);
        }
    }
}
