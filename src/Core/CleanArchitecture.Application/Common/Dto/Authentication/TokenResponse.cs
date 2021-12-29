using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Dto.Authentication
{
    public class TokenResponse
    {
        public TokenResponse(UserView userView,List<string> roles,string token)
        {
            Id = userView.Id;
            EmailAddress = userView.Email;
            Token = token;
            Roles = roles;
        }

        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Token { get; private set; }
        public List<string> Roles { get; private set; }
    }
}
