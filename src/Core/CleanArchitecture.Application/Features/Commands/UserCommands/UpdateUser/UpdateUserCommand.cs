using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Interfaces.Repository;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.UserCommands.UpdateUser
{
    [Authorize(Roles = "Administrator", Claims = "User.Update")]
    public class UpdateUserCommand : IRequest<ServiceResult<bool>>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled{ get; set; }

        public Guid StoreId { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceResult<bool>>
    {
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IIdentityService identityService,IMapper mapper)
        {
            this.identityService = identityService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                return new ServiceResult<bool>(false, new string[] { "Satış temsilcisi bulunamadı." }, false);
            }

            //user.UserName = request.UserName;
            //user.FirstName = request.FirstName;
            //user.LastName = request.LastName;
            //user.StoreId = request.StoreId;

            await identityService.UpdateUserAsync(mapper.Map<User>(request));

            return new ServiceResult<bool>(true, new string[] { },true);
        }
    }
}
