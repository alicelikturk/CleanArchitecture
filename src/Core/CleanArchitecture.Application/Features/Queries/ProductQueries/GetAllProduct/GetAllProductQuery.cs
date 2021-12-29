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
 
namespace CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetAllProduct
{
    [Authorize(Roles = "Administrator", Claims = "")]
    [Authorize(Roles = "Manager", Claims = "Product.List")]
    [Authorize(Roles = "User", Claims = "Product.List")]
    public class GetAllProductQuery:IRequest<ServiceResult<List<ProductView>>>
    {

    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ServiceResult<List<ProductView>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository,IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<List<ProductView>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync();
            var viewModel = mapper.Map<List<ProductView>>(products);
            return new ServiceResult<List<ProductView>>(true, new string[] { },viewModel);
        }
    }
}
