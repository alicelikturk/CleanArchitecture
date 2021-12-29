using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Queries.UserQueries.GetAllUser
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "User.List")]
    public class GetAllUserQuery : IRequest<ServiceResult<List<UserView>>>
    {
    }
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, ServiceResult<List<UserView>>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GetAllUserQueryHandler(IIdentityService identityService, IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<List<UserView>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await identityService.GetAllUserAsync();
            var viewModel = mapper.Map<List<UserView>>(users);
            return new ServiceResult<List<UserView>>(true, new string[] { }, viewModel);
        }
    }
}
