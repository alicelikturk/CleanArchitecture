using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Queries.UserQueries.GetUserById
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "User.List")]
    public class GetUserByIdQuery:IRequest<ServiceResult<UserDetailView>>
    {
        public Guid Id { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ServiceResult<UserDetailView>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(IIdentityService identityService,IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<UserDetailView>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.Id);
            var userRoles = await identityService.GetUserRolesAsync(user);
            var userRolesPermissions = await identityService.GetClaimsByRolesAsync(userRoles);
            var viewModel = mapper.Map<UserDetailView>(user);
            viewModel.Roles = userRoles;
            viewModel.Permissions = userRolesPermissions;

            return new ServiceResult<UserDetailView>(true, new string[] { }, viewModel);
        }
    }
}
