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

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.CreateRole
{
    [Authorize(Roles = "Administrator", Claims = "Role.Create")]
    public class CreateRoleCommand:IRequest<ServiceResult<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ServiceResult<Guid>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public CreateRoleCommandHandler(IIdentityService identityService,IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var userId = await identityService.CreateRoleAsync(mapper.Map<UserRole>(request));

            return new ServiceResult<Guid>(true, new string[] { }, userId);
        }
    }
}
