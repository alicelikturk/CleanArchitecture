using AutoMapper;
using MediatR;
using CleanArchitecture.Application.Common.Dto.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Token;
using CleanArchitecture.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Features.Commands.AuthenticateCommands.Login
{
    public class LoginCommand : TokenRequest, IRequest<ServiceResult<TokenResponse>>
    {
        public string IpAddress { get; set; }
    }
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<TokenResponse>>
    {
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public LoginCommandHandler(ITokenService tokenService, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.mapper = mapper;
        }
        public async Task<ServiceResult<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            TokenResponse tokenResponse = await tokenService.Authenticate(request, request.IpAddress);
            if (tokenResponse == null)
            {
                return new ServiceResult<TokenResponse>(false, new string[] { "Giriş başarısız." }, null);
            }
            return new ServiceResult<TokenResponse>(true, new string[] { }, tokenResponse);
        }
    }
}
