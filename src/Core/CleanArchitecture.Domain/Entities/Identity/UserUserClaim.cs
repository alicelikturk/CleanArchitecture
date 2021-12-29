using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.Identity
{
    public class UserUserClaim : IdentityUserClaim<Guid>, IEntity
    {
    }
}
