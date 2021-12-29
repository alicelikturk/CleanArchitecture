using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Identity
{
    public class UserRole : IdentityRole<Guid>, IEntity
    {
        public UserRole()
        {

        }
        public UserRole(string roleName)
            :base(roleName)
        {

        }

        public bool IsDefault { get; set; }
        public string Description { get; set; }
    }
}
