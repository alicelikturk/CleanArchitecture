using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Security
{
    public class CustomAuthorizationPolicyHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return;
            }

            var claims = context.User.Claims.Where(x => x.Type == requirement.PolicyType &&
                                                        x.Value == "true" &&
                                                        x.Issuer == "LOCAL AUTHORITY");


            if (claims.Any() && (context.User.IsInRole("Administrator") || context.User.IsInRole("User")))
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}
