using FluentValidation;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.ProductCommands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ürün ismi belirtilmeli.")
                .MaximumLength(200).WithMessage("Ürün ismi 200 karakterden fazla olamaz.")
                .MustAsync(BeUniqueName).WithMessage("Belirtilen ürün ismi zaten mevcut");
            RuleFor(x => x.Description)
               .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olmalıdır.");
            this.productRepository = productRepository;
        }
        private async Task<bool> BeUniqueName(UpdateProductCommand model,string name, CancellationToken cancellationToken)
        {
            return await productRepository
                .AllByConditionAsync(x => x.Id != model.Id, y => y.Name != name);
        }
    }
}
