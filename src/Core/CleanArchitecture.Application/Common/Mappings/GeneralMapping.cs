using AutoMapper;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Features.Commands.PermissionStoreCommands.CreatePermissionStore;
using CleanArchitecture.Application.Common.Features.Commands.ProductCommands.CreateProduct;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Product, ProductView>()
                .ReverseMap();

            CreateMap<Product, CreateProductCommand>()
                .ReverseMap();

            CreateMap<PermissionStore, PermissionStoreView>()
                .ReverseMap();

            CreateMap<PermissionStore, CreatePermissionStoreCommand>()
                .ReverseMap();

        }
    }
}
