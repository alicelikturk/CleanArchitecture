using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Application.Common.Models;
using System.Linq;

namespace CleanArchitecture.Persistence.Identity.Services
{
    public static class IdentityResultExtensions
    {
        public static ApplicationResult ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? ApplicationResult.Success()
                : ApplicationResult.Failure(result.Errors.Select(x => x.Description));
        }
    }
}
