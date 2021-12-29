using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.CreateUser
{
    [Authorize(Roles = "Administrator", Claims = "User.Create")]
    public class CreateUserCommand : IRequest<ServiceResult<Guid>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid StoreId { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceResult<Guid>>
    {
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public CreateUserCommandHandler(IMapper mapper,IIdentityService identityService)
        {
            this.mapper = mapper;
            this.identityService = identityService;
        }
        public async Task<ServiceResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId=await identityService.CreateUserAsync(mapper.Map<User>(request));

            return new ServiceResult<Guid>(true, new string[] { }, userId);
        }
    }
}
