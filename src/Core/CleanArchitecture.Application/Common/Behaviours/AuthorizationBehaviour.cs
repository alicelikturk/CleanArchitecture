using MediatR;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using CleanArchitecture.Application.Common.Interfaces.Identity;
using CleanArchitecture.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IIdentityService identityService;

        public AuthorizationBehaviour(ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            this.currentUserService = currentUserService;
            this.identityService = identityService;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = (IEnumerable<AuthorizeAttribute>)request.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true);

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (currentUserService.UserId == Guid.Empty)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(x => !string.IsNullOrWhiteSpace(x.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    var authorized = false;

                    foreach (var roles in authorizeAttributesWithRoles.Select(x => x.Roles.Split(',')))
                    {
                        foreach (var role in roles)
                        {
                            var isInRole = await identityService.IsInRoleAsync(currentUserService.UserId, role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }
                    }

                    // Must be a member of at least one role in roles
                    if (!authorized)
                    {
                        throw new ForbiddenAccessException();
                    }
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(x => !string.IsNullOrWhiteSpace(x.Claims));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var claim in authorizeAttributesWithPolicies.Select(x => x.Claims))
                    {
                        var autorized = await identityService.IsInClaimAsync(currentUserService.UserId, claim);

                        if (!autorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }

            }

            // User is authorized / authorization not required
            return await next();

        }
    }
}
