using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Application.Common.Dto;
using CleanArchitecture.Application.Common.Dto.Authentication;
using CleanArchitecture.Application.Common.Interfaces.Token;
using CleanArchitecture.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly Token token;

        public TokenService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<Token> tokenOptions)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.token = tokenOptions.Value;
        }
        public async Task<TokenResponse> Authenticate(TokenRequest request, string ipAddress)
        {
            if (await IsValidUser(request.UserName, request.Password))
            {
                User user = await GetUserByUserName(request.UserName);

                if (user != null && user.IsEnabled)
                {
                    List<string> roles = (await userManager.GetRolesAsync(user)).ToList();
                    var jwtToken = await GenerateJwtToken(user);

                    //RefreshToken refreshToken = GenerateRefreshToken(ipAddress);

                    //user.RefreshTokens.Add(refreshToken);
                    //await userManager.UpdateAsync(user);

                    return new TokenResponse(new UserView
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Id = user.Id
                    },
                        roles,
                        jwtToken
                        //,refreshToken
                        );
                }
            }
            return null;
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var roles = (await userManager.GetRolesAsync(user)).ToList();
            byte[] secret = Encoding.ASCII.GetBytes(token.Secret);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = token.Issuer,
                Audience = token.Audience,
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim("userId",user.Id.ToString()),
                    new Claim("fullName",user.FullName),
                    new Claim("userName",user.UserName),
                    new Claim("emailAddress",user.Email),
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim(ClaimTypes.Role,string.Join(',',roles)),
                }),
                Expires = DateTime.UtcNow.AddSeconds(token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = handler.CreateToken(descriptor);
            return handler.WriteToken(securityToken);
        }

        private async Task<User> GetUserByUserName(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public async Task<bool> IsValidUser(string username, string password)
        {
            User user = await GetUserByUserName(username);

            if (user == null)
            {
                return false;
            }

            SignInResult signInResult = await signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        public Task<TokenResponse> RefreshToken(string refreshToken, string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}
