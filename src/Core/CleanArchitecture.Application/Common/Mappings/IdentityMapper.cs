using AutoMapper;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.CreateRole;
using CleanArchitecture.Application.Common.Features.Commands.RoleCommands.UpdateRole;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.CreateUser;
using CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Identity;
using System.Security.Claims;

namespace CleanArchitecture.Application.Common.Mappings
{
    public class IdentityMapper : Profile
    {
        public IdentityMapper()
        {

            CreateMap<User, CreateUserCommand>()
               .ReverseMap();

            CreateMap<User, UpdateUserCommand>()
               .ReverseMap();

            CreateMap<UserRole, RoleView>()
               .ReverseMap();

            CreateMap<UserRole, CreateRoleCommand>()
               .ReverseMap();

            CreateMap<UserRole, UpdateRoleCommand>()
               .ReverseMap();

            CreateMap<Claim, PermissionStoreView>()
                .ForMember(x => x.ClaimType, y => y.MapFrom(z => z.Type))
                .ForMember(x => x.ClaimValue, y => y.MapFrom(z => z.Value))
                .ReverseMap();

            CreateMap<Claim, PermissionStore>()
               .ForMember(x => x.ClaimType, y => y.MapFrom(z => z.Type))
               .ForMember(x => x.ClaimValue, y => y.MapFrom(z => z.Value))
               .ReverseMap();

        }
    }
}
