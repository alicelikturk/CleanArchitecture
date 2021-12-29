using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Queries.RoleQueries.GetRoleById
{
    [Authorize(Roles = "Administrator")]
    public class GetRoleByIdQuery:IRequest<ServiceResult<RoleView>>
    {
        public Guid Id { get; set; }
    }
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ServiceResult<RoleView>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GetRoleByIdQueryHandler(IIdentityService identityService,IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<RoleView>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await identityService.GetRoleByIdAsync(request.Id);
            
            var rolePermissions =await identityService.GetPermissionsAsync(role);
            
            var viewModel = mapper.Map<RoleView>(role);

            viewModel.PermissionStoreViews = mapper.Map<List<PermissionStoreView>>(rolePermissions);

            return new ServiceResult<RoleView>(true, new string[] { }, viewModel);
        }
    }
}
