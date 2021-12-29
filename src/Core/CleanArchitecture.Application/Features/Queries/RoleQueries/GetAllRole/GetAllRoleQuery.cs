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

namespace CleanArchitecture.Application.Common.Features.Queries.RoleQueries.GetAllRole
{
    [Authorize(Roles = "Administrator")]
    public class GetAllRoleQuery : IRequest<ServiceResult<List<RoleView>>>
    {
    }
    public class GetAllRoleQueryHanfler : IRequestHandler<GetAllRoleQuery, ServiceResult<List<RoleView>>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GetAllRoleQueryHanfler(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<List<RoleView>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await identityService.GetAllRoleAsync();
            var viewModel = mapper.Map<List<RoleView>>(roles);
            return new ServiceResult<List<RoleView>>(true, new string[] { }, viewModel);
        }
    }
}
