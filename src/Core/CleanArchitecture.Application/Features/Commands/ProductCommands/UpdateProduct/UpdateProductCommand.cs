using MediatR;
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

namespace CleanArchitecture.Application.Common.Features.Commands.ProductCommands.UpdateProduct
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "Product.Update")]
    public class UpdateProductCommand:IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResult<bool>>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<ServiceResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return new ServiceResult<bool>(false, new string[] { "Ürün bulunamadı" },false);
            }

            product.Name = request.Name;
            product.Description = request.Description;

            await productRepository.UpdateAsync(product);

            return new ServiceResult<bool>(true, new string[] {},true);
        }
    }
}
