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

namespace CleanArchitecture.Application.Common.Features.Commands.ProductCommands.CreateProduct
{

    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "Product.Create")]
    public class CreateProductCommand : IRequest<ServiceResult<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResult<Guid>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);
            await productRepository.AddAsync(product);

            return new ServiceResult<Guid>(true, new string[] { },product.Id);
        }
    }

}
