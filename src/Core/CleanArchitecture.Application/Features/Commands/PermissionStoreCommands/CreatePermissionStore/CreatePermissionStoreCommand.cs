using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.PermissionStoreCommands.CreatePermissionStore
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "Permission.Create")]
    public class CreatePermissionStoreCommand:IRequest<ServiceResult<Guid>>
    {
        public Guid? ParentClaimId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
    public class CreatePermissionStoreCommandHandler : IRequestHandler<CreatePermissionStoreCommand, ServiceResult<Guid>>
    {
        private readonly IPermissionStoreRepository permissionStoreRepository;
        private readonly IMapper mapper;

        public CreatePermissionStoreCommandHandler(IPermissionStoreRepository permissionStoreRepository,IMapper mapper)
        {
            this.permissionStoreRepository = permissionStoreRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<Guid>> Handle(CreatePermissionStoreCommand request, CancellationToken cancellationToken)
        {
            var permissionStore = mapper.Map<Domain.Entities.PermissionStore>(request);
            permissionStore.ParentClaimId = request.ParentClaimId ?? Guid.Empty;
            permissionStore.ClaimValue = "true";
            await permissionStoreRepository.AddAsync(permissionStore);

            return new ServiceResult<Guid>(true, new string[] { }, permissionStore.Id);
        }
    }
}
