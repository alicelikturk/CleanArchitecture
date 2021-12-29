using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Persistence.Security
{
    public class CustomAuthorizationRequirement: IAuthorizationRequirement
    {
        /// <summary>
        /// Claim type
        /// </summary>
        public string PolicyType { get; set; }
        public CustomAuthorizationRequirement(string permission)
        {
            PolicyType = permission;
        }
    }
}