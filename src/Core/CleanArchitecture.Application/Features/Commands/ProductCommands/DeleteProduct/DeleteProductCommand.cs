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

namespace CleanArchitecture.Application.Common.Features.Commands.ProductCommands.DeleteProduct
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager", Claims = "Product.Delete")]
    public class DeleteProductCommand:IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResult<bool>>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<ServiceResult<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id);

            if (product==null)
            {
                return new ServiceResult<bool>(false, new string[] { "Ürün bulunamadı" },false);
            }

            await productRepository.DeleteAsync(product);

            return new ServiceResult<bool>(true, new string[] { },true);
        }
    }
}
