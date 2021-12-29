using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Queries.ProductQueries.GetProductById
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "Product.List")]
    [Authorize(Roles = "User", Claims = "Product.List")]
    public class GetProductByIdQuery : IRequest<ServiceResult<ProductView>>
    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResult<ProductView>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository,
            IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<ProductView>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);
            var viewModel = mapper.Map<ProductView>(product);
            return new ServiceResult<ProductView>(true, new string[] { },viewModel);
        }
    }
}
