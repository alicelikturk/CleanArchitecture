using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities
{
    /// <summary>
    /// as a claim
    /// </summary>
    public class PermissionStore:BaseEntity
    {
        public Guid ParentClaimId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
