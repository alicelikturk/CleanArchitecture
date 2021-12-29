using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Dto.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces.Token
{
    public interface ITokenService
    {
        Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress);

        Task<TokenResponse> RefreshToken(string refreshToken, string ipAddress);

        Task<bool> IsValidUser(string username, string password);
    }
}
