using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Queries.PermissionStoreQueries.GetAllPermissionStore
{
    [Authorize(Roles = "Administrator")]
    public class GetAllPermissionStoreQuery:IRequest<ServiceResult<List<PermissionStoreView>>>
    {

    }
    public class GetAllPermissionStoreQueryHandler : IRequestHandler<GetAllPermissionStoreQuery, ServiceResult<List<PermissionStoreView>>>
    {
        private readonly IPermissionStoreRepository permissionStoreRepository;
        private readonly IMapper mapper;

        public GetAllPermissionStoreQueryHandler(IPermissionStoreRepository permissionStoreRepository,IMapper mapper)
        {
            this.permissionStoreRepository = permissionStoreRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<List<PermissionStoreView>>> Handle(GetAllPermissionStoreQuery request, CancellationToken cancellationToken)
        {
            var permissionStores = await permissionStoreRepository.GetAllAsync();
            var viewModel = mapper.Map<List<PermissionStoreView>>(permissionStores);
            return new ServiceResult<List<PermissionStoreView>>(true, new string[] { }, viewModel);
        }
    }
}
