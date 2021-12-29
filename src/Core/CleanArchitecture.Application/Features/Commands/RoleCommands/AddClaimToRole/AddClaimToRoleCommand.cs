using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.RoleCommands.AddClaimToRole
{
    [Authorize(Roles = "Administrator", Claims = "Role.AddClaimToRole")]
    public class AddClaimToRoleCommand:IRequest<ServiceResult<bool>>
    {
        public Guid RoleId { get; set; }
        public Guid PermissionStoreId { get; set; }
    }
    public class AddClaimToRoleCommandHandler : IRequestHandler<AddClaimToRoleCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IPermissionStoreRepository permissionStoreRepository;

        public AddClaimToRoleCommandHandler(IIdentityService identityService,
            IMapper mapper,
            IPermissionStoreRepository permissionStoreRepository)
        {
            this.identityService = identityService;
            this.mapper = mapper;
            this.permissionStoreRepository = permissionStoreRepository;
        }
        public async Task<ServiceResult<bool>> Handle(AddClaimToRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await identityService.GetRoleByIdAsync(request.RoleId);

            var permissonStore = await permissionStoreRepository.GetByIdAsync(request.PermissionStoreId);
           // var claim = mapper.Map<Claim>(permissonStore);
            var newClaim = new Claim(permissonStore.ClaimType,"true");
            var result =await identityService.AddClaimToRole(role, newClaim);

            return new ServiceResult<bool>(result.Succeeded, result.Errors, result.Succeeded);
        }
    }
}
